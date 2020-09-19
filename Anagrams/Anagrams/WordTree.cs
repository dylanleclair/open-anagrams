using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{
    public class WordTree
    {
        Node root;
        List<string> words;


        public WordTree(string filename)
        {

            root = new Node(null, '0');

            string[] w = System.IO.File.ReadAllLines(filename);

            words = new List<string>(w);
            words.Sort();

            EvilMasterMind();

        }

        public void EvilMasterMind()
        {
            foreach (var word in words)
            {

                AddWord(word);

            }
        }


        public void AddWord(string word)
        {
            Node n = root;

            for (int i = 0; i<word.Length; i++)
            {
                if (n.HasChild(word[i])) {
                    n = n.Letters[n.IndexOf(word[i])];
                } else
                {
                    // create new node
                    n = n.AddChild(word[i]);
                    
                }

            }

            n.Accepting = true;

            for (int i = 0; i<word.Length; i++)
            {
                Console.WriteLine(n.Value + " " +n.Accepting);
                n = n.Parent;
            }


            

        }


        public bool FindWord(string word)
        {
            Console.WriteLine($"Finding: {word}");

            Node n = root;

            for (int i = 0; i<word.Length; i++)
            {
                if (n.HasChild(word[i]))
                {
                    // advance
                    n = n.Letters[n.IndexOf(word[i])];
                } else
                {
                    return false;
                }
            }

            if (n.Accepting)
            {
                return true;
            } else
            {
                return false;
            }

            

        }


    }






}
