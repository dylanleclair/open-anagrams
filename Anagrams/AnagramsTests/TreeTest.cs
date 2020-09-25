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

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void TestTrees()
        {
            string directory = "word-lists/words.txt";

            WordTree light = new LightWordTree(directory);
            WordTree fast = new FastWordTree(directory);

            TestTree(light);
            TestTree(fast);


        }
        
        public void TestTree(WordTree tree)
        {

            string directory = "word-lists/words.txt";

            string[] words = System.IO.File.ReadAllLines(directory);

            foreach (string word in words)
            {
                string lowerCase = word.ToLower();
                Assert.IsTrue(tree.FindWord(lowerCase));
            }


        }

        

    }
}