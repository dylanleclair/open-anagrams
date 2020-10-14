using Anagrams;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWordGames
{

    public enum AnagramsStates
    {
        setup,
        play,
        end
    }

    public class AnagramsGame : IGame
    {

        const string WORDLIST_PATH = "Content/words.wl";

        FastWordTree wordset { get; set; }
        String rootword { get; set; }
        Int32 score { get; set; }
        

        public AnagramsGame ()
        {

        }

        private FastWordTree chooseWordSet()
        {

            FastWordTree fast = new FastWordTree();

            List<String> f = WordTree.generateAnagramsWordset(WORDLIST_PATH, new Parameters(3, 6, 70));

            rootword = f[0];

            foreach (var word in f)
            { 
                fast.AddWord(word);
            }

            return fast;


        }


        public void setup()
        {
            // handles initialization
            wordset = chooseWordSet();
            rootword = WordLists.Shuffle(rootword);

        }

        public void play()
        {
            // handles one turn of the game
            throw new NotImplementedException();
        }

        public void end()
        {
            // handles ending / cleanup 
            throw new NotImplementedException();
        }

        public void printState()
        {
            throw new NotImplementedException();
        }
    }
}
