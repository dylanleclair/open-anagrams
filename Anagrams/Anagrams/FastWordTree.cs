using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{


    public class FastWordTree
    {
        FastNode root;
        


        public FastWordTree(string filename)
        {
            List<string> words;

            root = new FastNode(null, '0');

            string[] w = System.IO.File.ReadAllLines(filename);

            words = new List<string>(w);
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
            FastNode n = root;

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

            FastNode n = root;

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




    public class FastNode
    {


        // Properties
        public FastNode Parent { get; set; }
        public char Value { get; set; }

        public Dictionary<char, FastNode> Children { get; set; }

        public bool Accepting { get; set; }

        public FastNode(FastNode parent, char letter)
        {
            Parent = parent;
            Value = letter;
            Children = new Dictionary<char, FastNode>();
            Accepting = false;
        }

        public FastNode AddChild(char letter)
        {
            FastNode n = new FastNode(this, letter);
            Children.Add(letter, n);
            return n;
        }



    } // end of class





}
