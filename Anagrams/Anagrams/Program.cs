using System;

namespace Anagrams
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var tree = new WordTree("words.txt");

            while (true)
            {
                Console.Write("\n");
                string input = Console.ReadLine();
                Console.WriteLine(tree.FindWord(input));

            }

           

        }



    }
}
