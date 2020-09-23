using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{

    /// <summary>
    /// Dictionary based implementation of a word tree. 
    /// Featuring slightly faster lookups than a LightWordTree, this comes at the price of storage.
    /// </summary>
    public class FastWordTree : WordTree
    {

        public FastWordTree(string filename) : base(new FastNode(null,'0'),filename)
        {

        }

        public FastWordTree(FastNode root, string filename) :base(root,filename)
        {
        }
    } // end of FastWordTree class




    public class FastNode : Node, INode
    {

        public Dictionary<char, FastNode> Children { get; set; }

        public FastNode(FastNode parent,char letter) : base(parent,letter)
        {
            Children = new Dictionary<char, FastNode>();
        }

        public INode AddChild(char letter)
        {
            FastNode n = new FastNode(this, letter);
            Children.Add(letter, n);
            return n;
        }

        public INode GetChild(char letter)
        {
            if (Children.ContainsKey(letter))
            {
                return Children[letter];
            } else
            {
                return null;
            }

        }

    } // end of FastNode class


}
