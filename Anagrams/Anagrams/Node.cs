using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{
    public class Node
    {


        // Properties
        public Node Parent { get; set; }
        public char Value { get; set; }

        public Dictionary<char, Node> Children {get;set;}

        public bool Accepting { get; set; }

        public Node(Node parent, char letter)
        {
            Parent = parent;
            Value = letter;
            Children = new Dictionary<char, Node>();
            Accepting = false;
        }

        public Node AddChild (char letter)
        {
            Node n = new Node(this, letter);
            Children.Add(letter, n);
            return n;
        }



    } // end of class




} // end of namespace
