using Coldsteel;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class TradeOutputStation : BaseGamePiece
    {
        private SoundEffect _convert;

        public TradeOutputStation(Texture2D texture, SoundEffect convert)
        {
            this.Renderer = new SpriteRenderer(this, texture);
            _convert = convert;
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
            this.Interact(gameBoard, ship);
        }

        public override void Interact(GameBoard gameBoard, Ship ship)
        {            
            ship.ProcessRaw(gameBoard.Hud, _convert);
        }
    }
}
