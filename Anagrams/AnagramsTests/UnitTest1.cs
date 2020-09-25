using NUnit.Framework;
using Anagrams;
using System;
using System.Collections.Generic;

namespace AnagramsTests
{
    public class Tests
    {

        public List<string> words;
        WordTree light;
        WordTree fast;

        [SetUp]
        public void Setup()
        {

            string directory = "word-lists/words.txt";
            string[] w = System.IO.File.ReadAllLines(directory);

            light = new LightWordTree(directory);
            fast = new FastWordTree(directory);

            foreach (string word in w)
            {
                string lowerCase = word.ToLower();
                if (word.Length > 0)
                {
                    words.Add(word);
                }
            }

        }

        [Test]
        public void TestTreesBuild()
        {
            TestTree(light);
            TestTree(fast);
        }

        [Test]
        public void TestTreesPerms()
        {
            TestPermutations(light);
            TestPermutations(fast);
        }

        
        public void TestTree(WordTree tree)
        {
            foreach (string word in words)
            {
                Assert.IsTrue(tree.FindWord(word));
            }

        }

        public void TestPermutations(WordTree tree)
        {

            foreach (string word in words)
            {
                List<string> perms = tree.FindPermutations(word);

                foreach(string p in perms)
                {
                    Assert.IsTrue(tree.FindWord(p));
                }

            }

        }


    }
}