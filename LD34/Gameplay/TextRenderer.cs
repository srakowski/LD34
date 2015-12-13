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
    class TextRenderer : Renderer
    {
        private string _text = String.Empty;

        private Dictionary<char, Rectangle> _destinationRectangles;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private Texture2D _fontTexture;

        public TextRenderer(GameObject obj, Texture2D fontTexture)
            : base(obj)
        {
            this.Text = String.Empty;
            this._fontTexture = fontTexture;
            this._destinationRectangles = new Dictionary<char, Rectangle>();
            for (var c = 'a'; c <= 'z'; c++)
                _destinationRectangles[c] = new Rectangle(10 * (c - 'a'), 0, 10, 18);

            for (var c = 'A'; c <= 'Z'; c++)
                _destinationRectangles[c] = new Rectangle(10 * (c - 'A'), 18, 10, 18);

            char[] others = new char[] { ' ', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', ',', '?', '>', '<', ':' };
            for (var i = 0; i < others.Length; i++)
                _destinationRectangles[others[i]] = new Rectangle(10 * i, 36, 10, 18);
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            for (var i = 0; i < Text.Length; i++)
            {
                var c = Text[i];
                sb.Draw(_fontTexture,
                    this.GameObject.Transform.Position + new Vector2(i * 10, 0),
                    GetDestinationRectangle(c),
                    new Color(150,150,150), 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
        }

        private Rectangle GetDestinationRectangle(char c)
        {
            return _destinationRectangles[c];
        }
    }
}
