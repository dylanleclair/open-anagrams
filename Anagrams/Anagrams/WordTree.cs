using System;
using System.Collections.Generic;
using System.Text;

namespace Anagrams
{
    public interface WordTree
    {
        void AddWord();
        bool FindWord();

    }


    public interface INode
    {
        INode AddChild(char letter);
    }

    public abstract class Node
    {
        public Node Parent { get; set; }
        public char Value { get; set; }
        public bool Accepting { get; set; }

        public Node()
        {

        }

        public Node (Node parent, char letter)
        {
            Parent = parent;
            Value = letter;
            Accepting = false;
        }
    }

}
