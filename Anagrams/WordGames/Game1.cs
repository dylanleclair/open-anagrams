using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WordGames
{
    public class Game1 : Nez.Core
    {

        public Game1() : base(1920,1080, true)
        {

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            this.IsFixedTimeStep = true;

        }


        protected override void Initialize()
        {

            base.Initialize();


            Scene = new MasterScene();
        }

    }
}
