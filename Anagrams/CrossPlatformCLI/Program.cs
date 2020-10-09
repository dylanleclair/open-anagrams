using System;
using System.Collections.Generic;
using System.Linq;
using Anagrams;

namespace CrossPlatformCLI
{
    /// <summary>
    /// TODO: add logic to check if a word was already guessed.
    /// </summary>
    public class Program
    {

        public const string WORDSET_DIRECTORY = "wordsets";
        public const string DICTIONARY_DIRECTORY = "word-lists/words.words";

        static int Score { get; set; }

        static void Main(string[] args)
        {


            


            if (System.IO.Directory.Exists(WORDSET_DIRECTORY))
            {
                PlayAnagrams();
            } else
            {

                LightWordTree light = new LightWordTree(DICTIONARY_DIRECTORY);

                List<string> s = light.FindPermutations("lagoon");

                foreach (var item in s)
                {
                    Console.WriteLine(item);
                }

                light.GenerateWordSets(3, 6, 80);

                throw new Exception("No wordset files provided.");
            }



        }


        public static void PlayAnagrams()
        {

            List<string> wordsets = new List<string>(System.IO.Directory.EnumerateFiles(WORDSET_DIRECTORY));

            Random r = new Random();
            int rand = r.Next(0, wordsets.Count - 1);
            List<string> wordset = new List<string>(System.IO.File.ReadAllLines(wordsets[rand]));


            LightWordTree gameTree = new LightWordTree(wordsets[rand]);

            // THIS TAKES A LONG TIME!
            if (!System.IO.Directory.Exists(WORDSET_DIRECTORY))
            {
                gameTree.GenerateWordSets(3,6,75);
            }

            char[] characters = wordsets[rand].Substring(WORDSET_DIRECTORY.Length+1,6).ToCharArray();


            Console.WriteLine("Rootword: " + string.Concat(characters));

            string chars = "";

            foreach (var c in characters)
            {
                chars += c;
            }

            chars = Anagrams.Helpers.Shuffle(chars);

            string lol = "";

            foreach (var c in chars)
            {
                lol += c + " ";
            }

            Console.WriteLine(lol);

            while (true)
            {

                Console.WriteLine("\rPlease guess a word: \n");
                string input = Console.ReadLine().ToLower();
                input = input.Trim();


                if (input.Equals("s"))
                {
                    lol = "";

                    chars = Anagrams.Helpers.Shuffle(chars);

                    foreach (var c in chars)
                    {
                        lol += c + " ";
                    }


                    Console.WriteLine($"\nCharacters: {lol}");

                    continue;
                }

                bool result = gameTree.FindWord(input);

                string output = "";

                if (result)
                {
                    int score = 100 * input.Length * input.Length;
                    Score += score;
                    output += $"Correct! +{score}";
                } else
                {
                    output += $"Incorrect! :(";
                }

                Console.WriteLine($"\r{output}");

                Console.WriteLine($"\nCharacters: {lol}");

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
