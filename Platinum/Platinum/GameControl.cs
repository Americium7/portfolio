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
    public class GameControl
    {
        int counter;

        public GameControl()
        {
            //
        }

        public void Movement(Game g)
        {
            counter = 0;
            Core.ProcessEvents();
            g.Hero.PlayerSprite.CurrentFrame = g.Hero.Direction;
            if (Input.IsKeyPressed(Keys.VK_UP) && (counter < g.Hero.JumpLimit))
            {
                g.Hero.PlayerSprite.Y += -10;
                g.Hero.IsJumping = true;
                counter += 1; //counter restricts jump once reaching limit
            }
            else
            {
                g.Hero.IsJumping = false;
                g.Map.GravityEffect(g.Hero);// makes sprite fall
            }

            if (counter == g.Hero.JumpLimit) {
                //enables jump once sprite has landed
                if (MappyLoader.SpriteHasCollidedWithMapTile(g.Map.Map, g.Hero.PlayerSprite) && (Input.IsKeyPressed(Keys.VK_UP) == false))
                {
                    counter = 0;
                };
            };

            if (Input.IsKeyPressed(Keys.VK_LEFT) && (g.Hero.PlayerSprite.X > 0) && (MappyLoader.WillCollideOnSide(g.Map.Map, g.Hero.PlayerSprite) != CollisionSide.Right))
            {
                g.Hero.PlayerSprite.X += -g.Speed;
                g.Hero.Direction = 0;
            };

            //MoveSpriteOutOfTile (game.map, game.hero.playerSprite, Round(game.hero.playerSprite.X+SPEED), Round(game.hero.playerSprite.Y));

            if (Input.IsKeyPressed(Keys.VK_RIGHT) &&(g.Hero.PlayerSprite.X + g.Hero.Width < Core.ScreenWidth()) && (MappyLoader.WillCollideOnSide(g.Map.Map, g.Hero.PlayerSprite) != CollisionSide.Left))
            {
                g.Hero.PlayerSprite.X += g.Speed;
                g.Hero.Direction = 1;
            };
        }

    }
}
