using System;
using System.Drawing;

using SwinGame;
using Graphics = SwinGame.Graphics;
using Bitmap = SwinGame.Bitmap;
using Font = SwinGame.Font;
using FontStyle = SwinGame.FontStyle;
using Event = SwinGame.Event;
using CollisionSide = SwinGame.CollisionSide;
using Sprite = SwinGame.Sprite;

using GameResources;

namespace Platinum
{
    public static class GameLogic
    {
        public static void RunGame()
        {
            
            //Open a new Graphics Window
            Core.OpenGraphicsWindow("Game", 800, 600);
            //Open Audio Device
            Audio.OpenAudio();
            //Load Resources
            Resources.LoadResources();
            int gamespeed = 3;
            Game game1 = new Game(gamespeed);
            GameControl control = new GameControl();
            game1.Setup();

            //Game Loop
            do
            {
                //Clears the Screen to Black
                Graphics.ClearScreen();
                game1.DrawGame();
                control.Movement(game1);

                //Refreshes the Screen and Processes Input Events
                Core.RefreshScreen();
                Core.ProcessEvents();
            } while (!Core.WindowCloseRequested());

            //Free Resources and Close Audio, to end the program.
            Resources.FreeResources();
            Audio.CloseAudio();
        }
    }
}
