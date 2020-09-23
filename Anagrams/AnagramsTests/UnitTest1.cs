using NUnit.Framework;
using Anagrams;
using System;
using System.Collections.Generic;

namespace AnagramsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestFastTree(WordTree w)
        {

            string directory = "word-lists/words.txt";
            WordTree fast = new FastWordTree(directory);
            string[] words = System.IO.File.ReadAllLines(directory);

            List<string> w = new List<string>(words);

            foreach (string word in w)
            {
                string lowerCase = word.ToLower();
                Assert.IsTrue(fast.FindWord(lowerCase));
            }

        }
        
        [Test]
        public void TestLightTree()
        {


            string[] words = System.IO.File.ReadAllLines(directory);

            string directory = "word-lists/words.txt";
            WordTree light = new LightWordTree(directory);
            WordTree fast = new FastWordTree(directory);




        }

    }
}