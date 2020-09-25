using System;
using System.Collections.Generic;
using System.Linq;

namespace Anagrams
{
    public interface IWordTree
    {
        void AddWord(string word);
        bool FindWord(string word);

    }

    public abstract class WordTree : IWordTree
    {

        public INode Root { get; set; }

        // Constructors

        public WordTree()
        {

        }

        public WordTree(INode root, string filename)
        {

            Root = root;

            List<string> words;
            string[] w = System.IO.File.ReadAllLines(filename);

            string dir = "wordsbylength";


            System.IO.Directory.CreateDirectory(dir);


            words = new List<string>(w);
            words.Sort();


            Dictionary<int,List<string>> wordsbylength = new Dictionary<int, List<string>>();

            Console.WriteLine(wordsbylength.Count);
            foreach (var word in words)
            {
                string lower = word.ToLower();
                Console.WriteLine(lower);
                

                if (lower.Length > 0)
                {

                    if (!wordsbylength.ContainsKey(lower.Length))
                    {
                        wordsbylength.Add(lower.Length,new List<string>());
                    } else
                    {
                        wordsbylength[lower.Length].Add(lower);
                    }
                    AddWord(lower);

                }


            }

            
            foreach (KeyValuePair<int, List<string>> entry in wordsbylength)
            {
                // do something with entry.Value or entry.Key
                System.IO.File.WriteAllLines(dir + "/word" + entry.Key + ".txt", entry.Value);
                Console.WriteLine($"words of length {entry.Key} = {entry.Value.Count}");
            }


        }

        // Methods


        public virtual void AddWord(string word)
        {

            INode n = Root;

            word = word.ToLower();


            int len = word.Length;

            for (int i = 0; i< len ; i++)
            {
                INode check = n.GetChild(word[i]);

                if (check != null)
                {
                    n = check;
                } else
                {
                    n = n.AddChild(word[i]);
                }
            }


            n.Accepting = true;

        }

        public virtual bool FindWord(string word)
        {
            Console.WriteLine($"Finding: {word}\n");

            INode n = Root;

            for (int i = 0; i<word.Length;i++)
            {
                INode check = n.GetChild(word[i]);
                if (check != null)
                {
                    n = check;
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


    } // END OF WORDTREE CLASS




    public interface INode
    {
        INode Parent { get; set; }
        char Value { get; set; }
        bool Accepting { get; set; }

        INode AddChild(char letter);
        
        INode GetChild(char letter);

    }

    public abstract class Node
    {
        public INode Parent { get; set; }
        public char Value { get; set; }
        public bool Accepting { get; set; }

        public Node()
        {

        }

        public Node(INode parent, char letter)
        {
            Parent = parent;
            Value = letter;
            Accepting = false;
        }

    }

}
