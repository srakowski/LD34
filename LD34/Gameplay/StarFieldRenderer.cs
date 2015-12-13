using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD34.Gameplay
{
    class StarFieldRenderer : Renderer
    {
        private Camera _camera = null;

        private int _width = 0;

        private int _height = 0;

        private Texture2D _starTexture = null;

        private Star[] _stars = null;

        public StarFieldRenderer(GameObject obj, Texture2D starTexture,
            Camera camera, int width, int height)
            : base(obj)
        {
            this._starTexture = starTexture;
            this._camera = camera;
            this._width = width;
            this._height = height;            
        }

        public void InitializeStars(int starCount)
        {
            var center = new Vector2(this.GameObject.ScreenManager.GraphicsDevice.Viewport.Width * 0.5f,
                            this.GameObject.ScreenManager.GraphicsDevice.Viewport.Height * 0.5f);
            var minX = (int)center.X - (int)(_width);
            var maxX = (int)center.X + (int)(_width);
            var minY = (int)center.Y - (int)(_width);
            var maxY = (int)center.Y + (int)(_width);

            this._stars = new Star[starCount];
            Random r = new Random();
            for (var s = 0; s < starCount; s++)
            {
                var pos = new Vector2(r.Next(minX, maxX), r.Next(minY, maxY));
                var g = r.Next(100, 200);
                this._stars[s] = new Star()
                {
                    position = pos,
                    color = new Color(g, g, g, r.Next(100, 200)),
                    scale = (float)r.NextDouble() + 1f,
                    rotation = (float)r.NextDouble()
                };
            }
        }

        internal void RotateStars(Vector2 origin, Matrix rotationMatrix)
        {
            var center = new Vector2(this.GameObject.ScreenManager.GraphicsDevice.Viewport.Width * 0.5f,
                this.GameObject.ScreenManager.GraphicsDevice.Viewport.Height * 0.5f);
            var minX = (int)center.X - (int)(_width);
            var maxX = (int)center.X + (int)(_width);
            var minY = (int)center.Y - (int)(_width);
            var maxY = (int)center.Y + (int)(_width);
            for (var s = 0; s < _stars.Length; s++)
            {
                _stars[s].position = Vector2.Transform(_stars[s].position - origin, rotationMatrix) + origin;
                if (_stars[s].position.X < minX)
                    _stars[s].position.X += _width * 2;
                else if (_stars[s].position.X > maxX)
                    _stars[s].position.X -= _width * 2;

                if (_stars[s].position.Y < minY)
                    _stars[s].position.Y += _width * 2;
                else if (_stars[s].position.Y > maxY)
                    _stars[s].position.Y -= _width * 2;
            }
        }

        public void ShiftStars(Vector2 distance)
        {
            var center = new Vector2(this.GameObject.ScreenManager.GraphicsDevice.Viewport.Width * 0.5f,
                this.GameObject.ScreenManager.GraphicsDevice.Viewport.Height * 0.5f);
            var minX = (int)center.X - (int)(_width);
            var maxX = (int)center.X + (int)(_width);
            var minY = (int)center.Y - (int)(_width);
            var maxY = (int)center.Y + (int)(_width);
            for (var s = 0; s < _stars.Length; s++)
            {
                _stars[s].position += distance * (_stars[s].color.A / 40);

                if (_stars[s].position.X < minX)
                    _stars[s].position.X += _width * 2;
                else if (_stars[s].position.X > maxX)
                    _stars[s].position.X -= _width * 2;

                if (_stars[s].position.Y < minY)
                    _stars[s].position.Y += _width * 2;
                else if (_stars[s].position.Y > maxY)
                    _stars[s].position.Y -= _width * 2;
            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            foreach (var star in _stars)
                sb.Draw(_starTexture,
                    star.position,
                    null,
                    star.color,
                    star.rotation,
                    Vector2.Zero,
                    star.scale,
                    SpriteEffects.None,
                    0f);
        }
    }
}
