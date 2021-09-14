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
    public class Hero
    {
        int _height;
        int _width;
        int _direction;
        Sprite _playerSprite;
        int _jumpLimit;
        bool _isJumping;

        public Hero(string spritename, int limit, int x, int y)
        {
            _playerSprite = Graphics.CreateSprite(Resources.GameImage(spritename), 1, 2, 40, 50);
            _playerSprite.X = x;
            _playerSprite.Y = y;

            _jumpLimit = limit;
            _height = 50;
            _width = 40;
            IsJumping = false;
            Direction = 1;
        }

        public int JumpLimit
        {
            get
            {
                return _jumpLimit;
            }
        }

        public Sprite PlayerSprite
        {
            get
            {
                return _playerSprite;
            }
        }

        public bool IsJumping
        {
            get
            {
                return _isJumping;
            }

            set
            {
                _isJumping = value;
            }
        }

        public int Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }
        }
    }
}
