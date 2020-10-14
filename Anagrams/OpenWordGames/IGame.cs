using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWordGames
{

    public interface IGame
    {
        /**
      * Performs setup operations for a Game.
      */
        void setup();

        /**
         * Implements a game's game loop, using a Scanner to get command line input.
         * @param s - the Scanner which will provide input to the Game.
         */
        void play();

        /**
         * Performs cleanup operations for a game (if any)
         */
        void end();

        /**
         * Prints a summary of the state of the game. 
         */
        void printState();



    }

}
