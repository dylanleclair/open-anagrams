using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{
    /// <summary>
    /// List based implementation of a word tree. A little lighter on storage, but comes with slightly slower lookups.
    /// Considering most words aren't terribly long, this is generally a better choice.
    /// </summary>
    public class LightWordTree
    {
        /// <summary>
        /// The root node of the tree.
        /// </summary>
        LightNode root;
 
        /// <summary>
        /// Constructs a LightWordTree from a text file containing words to inject into it. 
        /// 
        /// In the text file, words should be placed on seperate lines. 
        /// 
        /// The root node has a value of 0, with a null Parent.
        /// </summary>
        /// <param name="filename">The path to the text file</param>
        public LightWordTree(string filename)
        {
            root = new LightNode(null, '0');

            string[] w = System.IO.File.ReadAllLines(filename);

            List<string> words = new List<string>(w);
            words.Sort();

            PopulateTree(words);
            
            Console.WriteLine("Number of words loaded: " + words.Count);

        }
        
        /// <summary>
        /// A helper function for constructing a light word tree, this function simply adds words from a list into the tree.
        /// </summary>
        /// <param name="words">A list of words to be added to the tree.</param>
        public void PopulateTree(List<string> words)
        {
            foreach (var word in words)
            {
                AddWord(word);
            }
        }


        /// <summary>
        /// Adds a word to the LightWordTree.
        /// </summary>
        /// <param name="word">A word to add to the tree.</param>
        public void AddWord(string word)
        {
            LightNode n = root;

            word = word.ToLower();

            for (int i = 0; i<word.Length; i++)
            {

                LightNode check = n.GetChild(word[i]);

                if (check != null) {
                    n = check;
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

        /// <summary>
        /// Searches the LightWordTree for a word.
        /// 
        /// Returns true if the word is in the tree, false otherwise.
        /// </summary>
        /// <param name="word">The word to be searched for.</param>
        /// <returns>True if the word exists in the tree, false if the word does not.</returns>
        public bool FindWord(string word)
        {
            Console.WriteLine($"Finding: {word}");

            LightNode n = root;

            for (int i = 0; i<word.Length; i++)
            {

                LightNode check = n.GetChild(word[i]);

                if (check != null)
                {
                    // advance
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


    } // End of LightWordTree class

    /// <summary>
    /// Represents a Node in a LightWordTree.
    /// </summary>
    public class LightNode : Node, INode 
    {

        // Properties
        // TODO: change necessary properties to fields.

        public List<LightNode> Children { get; set; }

        public LightNode(LightNode parent, char letter) : base(parent,letter)
        {
            Children = new List<LightNode>();
        }

        public INode AddChild(char letter)
        {
            LightNode n = new LightNode(this, letter);
            Children.Add(n);
            return n;
        }

        /// <summary>
        /// Retreives the desired Child of a LightNode, if it exists. Returns null otherwise.
        /// </summary>
        /// <param name="letter">The letter to check this LightNode's Children for.</param>
        /// <returns>The Child LightNode with Value == letter, null otherwise.</returns>
        public LightNode GetChild(char letter)
        {
            foreach (var item in Children)
            {
                if (item.Value == letter) return item;
            }
            return null;
        }

    } // End of LightNode class




}
