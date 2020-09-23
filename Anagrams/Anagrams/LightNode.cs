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
        public List<Node> Letters { get; set; }
        public bool Accepting { get; set; }

        
        public Node(Node parent, char letter)
        {
            Parent = parent;
            Value = letter;
            Letters = new List<Node>();
            Accepting = false;
        }

        public Node AddChild (char letter)
        {
            Node n = new Node(this, letter);
            Letters.Add(n);
            return n;
        }


        public bool HasChild(char letter)
        {
            foreach (var item in Letters)
            {
                if (item.Value == letter) return true;
            }
            return false;
        }

        public int IndexOf (char letter)
        {

            for (int i = 0; i<Letters.Count; i++)
            {
                if (letter == Letters[i].Value)
                {
                    return i;
                }
            }


            return -1;
        }

    }




}
