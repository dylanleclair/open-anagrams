using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{
    /// <summary>
    /// List based implementation of a word tree. A little lighter on storage, but comes with slightly slower lookups.
    /// Considering most words aren't terribly long, this is generally a better choice.
    /// </summary>
    public class LightWordTree : WordTree
    {

        public LightWordTree(string filename) : base(new LightNode(null,'0'),filename)
        {

        }

        public LightWordTree(LightNode root, string filename) : base(root, filename)
        {

        }

    } // End of LightWordTree class

    /// <summary>
    /// Represents a Node in a LightWordTree.
    /// </summary>
    public class LightNode : Node , INode
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

        
        public INode GetChild(char letter)
        {
            foreach (var item in Children)
            {
                if (item.Value == letter) return item;
            }
            return null;
        }

    } // End of LightNode class




}
