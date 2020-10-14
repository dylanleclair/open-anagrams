using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using Nez.UI;
using System.Collections.Generic;

namespace OpenWordGames
{
    // rename to anagrams scene
    internal class MasterScene : Scene
    {


        Settings settings;

        const int UIRENDERLAYER = 999;
        UICanvas canvas;

        public AnagramsGame Game { get; set; }
        public AnagramsStates State { get; set; }

        public List<string> lettersGuessed { get; set; }

        public BitmapFont Font { get; set; }

        public override void Initialize()
        {

            // SCREEN / DESIGN INITIALIZATION
            SetDesignResolution(1920, 1080, Scene.SceneResolutionPolicy.ExactFit);
            Screen.SetSize(1920, 1080);

            // GAME INITIALIZATION

            Game = new AnagramsGame();
            State = AnagramsStates.setup; 
            // UI INITIALIZATION

            AddRenderer(new ScreenSpaceRenderer(100, UIRENDERLAYER));
            canvas = CreateEntity("ui").AddComponent(new UICanvas());
            canvas.RenderLayer = UIRENDERLAYER;
            canvas.IsFullScreen = true;


            // CHOOSE FONT BASED ON SETTINGS

            // temporary settings constructor
            settings = new Settings();

            if (settings.OutlineText)
            {
                Font = Content.LoadBitmapFont("Content/fonts/outalpha.fnt");
            }
            else
            {
                Font = Content.LoadBitmapFont("Content/fonts/falseout.fnt");
            }



            Texture2D bkg = Content.LoadTexture("Content/waifu.jpg");

            Stage stage = canvas.Stage;
            Stack stack = stage.AddElement(new Stack());
            stack.FillParent = true;

            Table basic = new Table().Center();
            //basic.SetBackground(new SpriteDrawable(bkg));
            LabelStyle ls = new LabelStyle(Font, Color.White);
            basic.Add(new Label("Sample",ls));

            Container background = new Container();
            background.SetBackground(new SpriteDrawable(bkg));

            Container shader = new Container();
            shader.SetBackground(new PrimitiveDrawable(new Color(0, 0, 0, 200)));
            // make a setting that allows you to control the alpha on the shader.




            stack.Add(background);
            stack.Add(shader);
            stack.Add(basic);


            // END

            base.Initialize();
        }


        public override void Update()
        {


            /**
             * 
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
            */

            // game logic needs to be in here - port state type system from other game.


            // game logic 
            switch(State)
            {
                case (AnagramsStates.setup):
                    


                    break;
                case (AnagramsStates.play):
                    // if "enter" is pressed, validate input from ui / selected letters
                    // then, check that against the AnagramsGame wordtree.

                    // otherwise, scan for other inputs (ie: the letters in the word!)
                    // and update the table accordingly

                    // update the game timer


                    break;
                case (AnagramsStates.end):

                    // report the score, etc. 

                    // play again vs main menu
                    //@TODO: Main menu implementation lol



                    break;
            }





            //player.Position = new Vector2(player.Position.X + dir.X * 10, player.Position.Y + dir.Y * 10);
            



            base.Update();
        }


    }
}