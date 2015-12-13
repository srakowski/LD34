using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Coldsteel;
using Microsoft.Xna.Framework;

namespace LD34.Gameplay
{
    class HudConsole : GameObject
    {
        private const int LINES = 10;

        readonly TextRenderer[] _lines = new TextRenderer[LINES];

        private int _pos = LINES - 1;

        private Texture2D _fontTexture;

        public HudConsole(Texture2D fontTexture)
        {
            this._fontTexture = fontTexture;
        }

        public void InitializeHud()
        {
            for (var l = 0; l < _lines.Length; l++)
            {
                var lineGO = new GameObject();
                _lines[l] = new TextRenderer(lineGO, _fontTexture);                
                lineGO.Transform.Position = new Vector2(2, 4 + (_lines.Length - l - 1) * 18);
                lineGO.Renderer = _lines[l];
                this.AddGameObject(lineGO);
            }
        }

        public void Log(string text)
        {            
            if (_pos != 0)
            {                
                _lines[_pos].Text = text;
                _pos--;
            }
            else
            {
                for (var i = 0; i < _lines.Length - 1; i++)
                    _lines[i].Text = _lines[i + 1].Text;
                _lines[_pos].Text = text;
            }
        }
    }
}
