using System;

namespace Anagrams
{
    public class Program
    {


        static int Score { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Select a word list to use:");


            string[] lists = System.IO.Directory.GetFiles("word-lists");

            int selectedIndex;

            for (int i = 0; i < lists.Length;i++)
            {
                Console.WriteLine($"{i + 1}: {lists[i].Substring(11)}");
            }

            selectedIndex = Convert.ToInt32(Console.ReadLine());
            while ((selectedIndex <= 0 && selectedIndex >= lists.Length + 1))
            {
                selectedIndex = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\nGenerating word tree...\n");
            
            var tree = new FastWordTree(lists[selectedIndex -1]);

            while (true)
            {
                Console.Write("\n");
                string input = Console.ReadLine().ToLower();
                input = input.Trim();
                Console.WriteLine(ProcessGuess(input, tree.FindWord(input)));



                

            }






           

        }


        public static string ProcessGuess (string word, bool correct)
        {
            string output = "";

            if (correct)
            {

                int score = 100 * word.Length * word.Length;
                Score += score;
                output += $"Correct! +{score}!\n";
            } else
            {
                output += "$Incorrect!";
            }

            output += $"Your score is: {Score}\n";


            return output;
        }


    }
}
