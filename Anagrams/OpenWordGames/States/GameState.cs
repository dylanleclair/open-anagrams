using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWordGames.States
{
    public class GameState
    { 

        public interface IGameState
        {
            void OnEnter(IGame game);
            void OnExit(IGame game);
            void Update(IGame game);
            GameState HandleTransitions(IGame game);
        }

    }
}
