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
            Console.WriteLine("Number of words loaded: " + words.Count);

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

            word = word.ToLower();

            for (int i = 0; i<word.Length; i++)
            {

                if (n.Children.ContainsKey(word[i]))
                {
                    n = n.Children[word[i]];
                } else
                {
                    n.AddChild(word[i]);
                }

            }

            n.Accepting = true;

        }


        public bool FindWord(string word)
        {
            Console.WriteLine($"Finding: {word}");

            Node n = root;

            for (int i = 0; i<word.Length; i++)
            {
                if (n.Children.ContainsKey(word[i]))
                {
                    // advance
                    n = n.Children[word[i]];
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
