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
    public class MapControl
    {
        int _gravity;
        Map map;


        public MapControl()
        {
            _gravity = 8;
        }

        public Map Map
        {
            get
            {
                return map;
            }

            set
            {
                map = value;
            }
        }

        


        public void GravityEffect(Hero h)
        {
            if (((MappyLoader.SpriteHasCollidedWithMapTile(Map, h.PlayerSprite)) == false) && (h.IsJumping == false))
            {
                h.PlayerSprite.Y += _gravity;
            }
        }

        public void LoadMap(string mapname)
        {
            Map = MappyLoader.LoadMap(mapname);
        }
        
    }
}
