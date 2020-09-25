using System;
using System.Collections.Generic;
using System.Linq;

namespace Anagrams
{
    public interface IWordTree
    {
        void AddWord(string word);
        bool FindWord(string word);

        List<string> FindPermutations(string word);
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

            foreach (var word in words)
            {
                string lower = word.ToLower();
               
                

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


        public List<string> FindPermutations (string word)
        {
            List<string> output = new List<string>();

            FindPermutationsHelper(Root, word.ToList<char>(), output);

            return output;
        }

        public void FindPermutationsHelper(INode node, List<char> characters, List<string> permutations)
        {

            if (characters.Count > 0)
            {

                foreach (var c in characters)
                {
                    INode check = node.GetChild(c);
                    if (check != null)
                    {
                        List<char> copyCharacters = new List<char>(characters);
                        copyCharacters.Remove(c);
                        

                        if (check.Accepting)
                        {
                            permutations.Add(BubbleUp(check));
                        }

                        FindPermutationsHelper(check, copyCharacters, permutations);
                    }
                    
                }


            } else
            {

                permutations.Add(BubbleUp(node));
                
            }




        }



        public string BubbleUp(INode node)
        {
            List<char> output = new List<char>();

            INode n = node;

            while (n.Parent != null)
            {
                output.Add(n.Value);
                n = n.Parent;
            }

            output.Reverse();

            return new string(output.ToArray());

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
