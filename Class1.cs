using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    internal class Tribble
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        public Tribble(Texture2D texture, Rectangle rect, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
        }
        public Texture2D Texture
        {
            get { return _texture; }
        }
        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public bool Move(GraphicsDeviceManager graphics)
        {
            bool hitTrue = false;
            _rectangle.Offset(_speed);
            if (_rectangle.Right > graphics.PreferredBackBufferWidth + _rectangle.Size.X || _rectangle.Left < 0 - _rectangle.Size.X)
            {
                hitTrue = true;
                if (_rectangle.X > 1)
                {
                    _rectangle.X = 0 - _rectangle.Size.X;
                }
                else
                {
                    _rectangle.X = graphics.PreferredBackBufferWidth + _rectangle.Size.X;
                }
            }
            if (_rectangle.Bottom > graphics.PreferredBackBufferHeight + _rectangle.Size.Y || _rectangle.Top < 0 - _rectangle.Size.Y)
            {
                hitTrue = true;
                if (_rectangle.Y > 1)
                {
                    _rectangle.Y = 0 - _rectangle.Size.Y;
                }
                else
                {
                    _rectangle.Y = graphics.PreferredBackBufferWidth + _rectangle.Size.Y;
                }
            }
            return hitTrue;
        }
    }
}
