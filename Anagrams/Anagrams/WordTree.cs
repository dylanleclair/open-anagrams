using System;
using System.Collections.Generic;


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

            

            words = new List<string>(w);
            words.Sort();

            foreach (var word in words)
            {
                AddWord(word);
            }

            Console.WriteLine($"Generated tree with {words.Count} words");

        }

        // Methods


        public virtual void AddWord(string word)
        {

            INode n = Root;

            word = word.ToLower();

            for (int i = 0; i<word.Length; i++)
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


    }


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
