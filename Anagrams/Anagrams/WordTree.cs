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
        void GenerateWordSets(int minlength, int maxlength, int target);
    }

    public readonly struct Parameters
    {
        public readonly int minlength;
        public readonly int maxlength;
        public readonly int target;
    
        public Parameters (int minlength, int maxlength, int target)
        {
            this.minlength = minlength;
            this.maxlength = maxlength;
            this.target = target;

        }

        public bool Passes(string word)
        {
            int len = word.Length;
            if (len >= minlength && len <= maxlength)
                return true;
            else return false;
        }

    }

    public abstract class WordTree : IWordTree
    {

        public INode Root { get; set; }

        // Constructors

        public WordTree()
        {

        }

        public WordTree(string[] words)
        {
            foreach (var word in words)
            {
                this.AddWord(word);
            }
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

        /// <summary>
        /// Adds a word to a word tree. Will not modify word lists.
        /// </summary>
        /// <param name="word">The word to add to the tree.</param>
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

        /// <summary>
        /// Finds a word in the WordTree.
        /// </summary>
        /// <param name="word">The word to search for.</param>
        /// <returns></returns>
        public virtual bool FindWord(string word)
        {
            //Console.WriteLine($"Finding: {word}\n");

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
                BubbleUp(n);
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

        public static List<string> generateAnagramsWordset(String filename, Parameters parameters)
        {
            string[] words = System.IO.File.ReadAllLines(filename);

            List<String> maxLengthWords = new List<string>();
            LightWordTree light = new LightWordTree();

            foreach (var word in words)
            {
                light.AddWord(word);
                if (word.Length == parameters.maxlength)
                {
                    maxLengthWords.Add(word);
                }
            }

            List<String> wordlist = new List<string>();

            Random r = new Random();
            int index = r.Next(0, maxLengthWords.Count);
            string rootword = maxLengthWords[index];
            wordlist.Add(rootword);

            List<string> output;
            do
            {
                output = FilterPermutations(light.FindPermutations(rootword), parameters);
            } while (output.Count >= parameters.target);

            return output;

        }

        public static List<string> FilterPermutations(List<string> words, Parameters parameters)
        {
            List<string> output = new List<string>();

            foreach (var word in words)
            {
                if (parameters.Passes(word))
                {
                    output.Add(word);
                }
            }

            return output;

        }


        private void FindPermutationsHelper(INode node, List<char> characters, List<string> permutations)
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
                            string word = BubbleUp(check);
                            if (word.Length >= 2 && !permutations.Contains(word))
                            {
                                permutations.Add(word);
                            }

                            
                        }

                        FindPermutationsHelper(check, copyCharacters, permutations);
                    }
                    
                }


            } else
            {

                string word = BubbleUp(node);
                if (!permutations.Contains(word) && node.Accepting)
                    permutations.Add(word);
                
            }




        }

        /// <summary>
        /// Assumes a non-zero input on all inputs. Generates wordsets with to new "wordsets" directory. 
        /// </summary>
        /// <param name="minlength">The desired min length of words in the generated wordsets</param>
        /// <param name="maxlength">The desired max length of words in the generated wordsets</param>
        /// <param name="target">The minimum number of words required to create a wordset</param>
        public void GenerateWordSets(int minlength, int maxlength, int target)
        {

            if (minlength > maxlength || minlength < 0 || maxlength < 0 || target < 0)
            {
                throw new NotSupportedException("Invalid parameters.");
            }

            string inputdir = $"wordsbylength/word{maxlength}.txt";
            
            string[] words = System.IO.File.ReadAllLines(inputdir);
            System.IO.Directory.CreateDirectory("wordsets");
            foreach (string word in words)
            {
                List<string> perms = FindPermutations(word);
                List<string> output = new List<string>();

                foreach (string w in perms)
                {
                    // want to exclude single letters
                    if (!(w.Length < Math.Max(1,minlength)))
                    {
                        output.Add(w);
                    }
                    
                }



                if (output.Count >= target)
                {
                    string outputdir = $"wordsets/{word}.txt";
                    System.IO.File.WriteAllLines(outputdir, output);

                }

            }

        }


        /// <summary>
        /// "Bubbles up" to the top of the word tree, building the string represented from root-to-input node. 
        /// 
        /// The input node will be last letter in outputted word.
        /// </summary>
        /// <param name="node">The input node .</param>
        /// <returns></returns>
        private string BubbleUp(INode node)
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
