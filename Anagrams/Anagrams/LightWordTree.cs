using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{
    public class LightWordTree
    {
        Node root;
 
        public LightWordTree(string filename)
        {
            root = new Node(null, '0');

            string[] w = System.IO.File.ReadAllLines(filename);

            List<string> words = new List<string>(w);
            words.Sort();

            PopulateTree(words);
            
            Console.WriteLine("Number of words loaded: " + words.Count);

        }
        
        public void PopulateTree(List<string> words)
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
