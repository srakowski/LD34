using Coldsteel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class JumpGate : BaseGamePiece
    {
        private SoundEffect _jump;

        public override bool IsTraversable
        {
            get
            {
                return true;
            }
        }

        public JumpGate(Texture2D texture, SoundEffect _jump)
        {
            this.Renderer = new SpriteRenderer(this, texture);
            this._jump = _jump;
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {            
            if (ship.RightPoint == this.Slot.GBPos)            
                ship.MoveRight(gameBoard, () => { ship.Jump(); gameBoard.EndLevel(); _jump.Play(); });
            else if (ship.LeftPoint == this.Slot.GBPos)
                ship.MoveLeft(gameBoard, () => { ship.Jump(); gameBoard.EndLevel(); _jump.Play(); });            
        }
    }
}
