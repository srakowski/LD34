using Coldsteel;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace LD34.Gameplay
{
    class Pirate : BaseGamePiece
    {
        private int _startHull = 0;

        private int Hull { get; set; }

        private Random _r;

        private SoundEffect _exp;

        public Pirate(Texture2D texture, Random r, SoundEffect _exp)
        {
            this.Hull = this._startHull = r.Next(2, 8);
            this.Renderer = new SpriteRenderer(this, texture);
            this.RigidBody = new RigidBody(this);
            this._r = r;
            this._exp = _exp;
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
            var hitfor = _r.Next(ship.MaxDamage);
            this.Hull -= hitfor;
            hud.Log("You hit the pirate for " + hitfor.ToString() + " point of dmg");

            if (this.Hull <= 0)
            {            
                gameBoard.DestroyPeice(this);
                _exp.Play();
                hud.Log("The pirate is no more");

                var mrew = _r.Next(_startHull, _startHull * 80);
                if (mrew > 0)
                    ship.GiveMetals(mrew, gameBoard.Hud, false);

                var fRew = _r.Next(0, _startHull * 2);
                if (fRew > 0)
                    ship.GiveFuel(fRew, gameBoard.Hud);
            }
        }

        public override void Interact(GameBoard gameBoard, Ship ship)
        {
            this.Select(gameBoard.Hud, gameBoard, ship);
        }

        public override void BeginTurn(GameTime gameTime, GameBoard gameBoard, Ship ship)
        {
            if (this.IsAdjacentTo(ship.Slot.GBPos))
            {
                gameBoard.Hud.Log("The pirate attacks you");
                ship.Hit(_r.Next(1, 10), gameBoard.Hud);                
            }
            else
            {
                Point move = this.Slot.GBPos;

                if (ship.Slot.GBPos.X < this.Slot.GBPos.X) move.X--;
                else if (ship.Slot.GBPos.X > this.Slot.GBPos.X) move.X++;

                if (ship.Slot.GBPos.Y < this.Slot.GBPos.Y) move.Y--;
                else if (ship.Slot.GBPos.Y > this.Slot.GBPos.Y) move.Y++;

                this.TurnOver = false;
                gameBoard.MovePieceTo(move, this, () => { this.TurnOver = true; });                                
            }
        }
    }
}
