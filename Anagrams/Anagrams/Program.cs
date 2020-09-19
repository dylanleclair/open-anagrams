using System;

namespace Anagrams
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var tree = new WordTree("word-lists/" + "words.txt");

            while (true)
            {
                Console.Write("\n");
                string input = Console.ReadLine().ToLower();
                input = input.Trim();
                Console.WriteLine(tree.FindWord(input));

            }

           

        }



    }
}
