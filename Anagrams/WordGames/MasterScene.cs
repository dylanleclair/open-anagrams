using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace WordGames
{
    internal class MasterScene : Scene
    {

        Entity player;

        public override void Initialize()
        {
            SetDesignResolution(1920, 1080, Scene.SceneResolutionPolicy.ExactFit);
            Screen.SetSize(1920, 1080);

            var sampleTex = Content.LoadTexture("Content/waifu.jpg");
            player = CreateEntity("fake", new Vector2(Screen.Width / 2, Screen.Height / 2));

            player.AddComponent(new SpriteRenderer(sampleTex));
            

            base.Initialize();
        }


        public override void Update()
        {
            Vector2 dir = new Vector2();
            if (Input.IsKeyDown(Keys.Up))
            {
                dir.Y = -1;
            } else if (Input.IsKeyDown(Keys.Down)) {
                dir.Y = 1;
            }
            if (Input.IsKeyDown(Keys.Left)) {
                dir.X = -1;
            } else if (Input.IsKeyDown(Keys.Right))
            {
                dir.X = 1;
            }

            player.Position = new Vector2(player.Position.X + dir.X * 10, player.Position.Y + dir.Y * 10);
            



            base.Update();
        }


    }
}