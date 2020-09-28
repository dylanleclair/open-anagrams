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
        public void TestTrees()
        {
            string directory = "word-lists/words.words";

            List<string> w = new List<string>();

            string[] words = System.IO.File.ReadAllLines(directory);

            foreach (string word in words)
            {
                string lowerCase = word.ToLower();
                w.Add(lowerCase);
            }

            WordTree light = new LightWordTree(directory);
            WordTree fast = new FastWordTree(directory);

            TestBuildTree(light, w);
            TestBuildTree(fast, w);
            TestPermutations(fast, w);

        }
        
        public void TestBuildTree(WordTree tree, List<string> words)
        {

            foreach (string word in words)
            {
                string lowerCase = word.ToLower();
                Assert.IsTrue(tree.FindWord(lowerCase));
            }


        }

        
        public void TestPermutations(WordTree tree, List<string> words)
        {

            for (int i = 0; i < words.Count; i += 3000)
            {

                foreach (var w in tree.FindPermutations(words[i])) 
                {
                    Assert.IsTrue(tree.FindWord(w));
                }

                
            }

        }


    }
}