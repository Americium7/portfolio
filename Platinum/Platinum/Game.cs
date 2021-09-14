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
    public class Game
    {
        private MapControl _map;
        private Hero _hero;
        private Timer gTimer;
        private int _speed;

        public Game(int speed)
        {
            Map = new MapControl();
            _speed = speed;
            
        }
        public MapControl Map
        {
            get
            {
                return _map;
            }

            set
            {
                _map = value;
            }
        }

        public Hero Hero
        {
            get
            {
                return _hero;
            }

            set
            {
                _hero = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }
        }

        public void DrawGame()
        {
            Graphics.ClearScreen();
            MappyLoader.DrawMap(Map.Map);
            Graphics.DrawSprite(Hero.PlayerSprite);
            Graphics.UpdateSprite(Hero.PlayerSprite);
            //Text.DrawText("Score: " + IntToStr(score), ColorBlack, 0, 0);
            Text.DrawFramerate(0, 0, Resources.GameFont("Courier"));
            Core.RefreshScreen(60);//max fps, is proportional to speed of game, leave as 60
        }

        public void Setup()
        {
            gTimer = new SwinGame.Timer();
            gTimer.Start();

            Map.LoadMap("test");
            int x = MappyLoader.EventPositionX(Map.Map, Event.Event24, 0);
            Hero = new Hero("hero", 10, x,
                MappyLoader.EventPositionX(Map.Map, Event.Event24, 0));

        }
    }
}
