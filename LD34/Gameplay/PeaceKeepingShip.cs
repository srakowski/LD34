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
    class PeaceKeepingShip : BaseGamePiece
    {
        private int _startHull;

        private int Hull { get; set; }

        private bool Hostile { get; set; } = false;

        private Random _r = null;

        private SoundEffect _exp;

        public PeaceKeepingShip(Texture2D texture, Random r, SoundEffect _exp)
        {
            this.Hull = _startHull = r.Next(2, 16);
            this.Renderer = new SpriteRenderer(this, texture);
            this.RigidBody = new RigidBody(this);
            this._r = r;
            this._exp = _exp;
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
            var hitfor = _r.Next(ship.MaxDamage);
            this.Hull -= hitfor;
            hud.Log("You hit the Peace Keeper for " + hitfor.ToString() + " point of dmg");

            if (!this.Hostile)
            {
                this.Hostile = true;
                hud.Log("The Peace Keeper is now hostile");
            }

            if (this.Hull <= 0)
            {                              
                hud.Log("The Peace Keeper is no more");
                _exp.Play();

                var mrew = _r.Next(_startHull, _startHull * 100);
                if (mrew > 0)
                    ship.GiveMetals(mrew, gameBoard.Hud, false);

                var fRew = _r.Next(0, _startHull * 10);
                if (fRew > 0)
                    ship.GiveFuel(fRew, gameBoard.Hud);

                gameBoard.DestroyPeice(this);
            }
        }

        public override void Interact(GameBoard gameBoard, Ship ship)
        {
            this.Select(gameBoard.Hud, gameBoard, ship);
        }

        public override void BeginTurn(GameTime gameTime, GameBoard gameBoard, Ship ship)
        {
            if (Hostile)
            {
                if (this.IsAdjacentTo(ship.Slot.GBPos))
                {
                    gameBoard.Hud.Log("A Peace Keeper attacks you");
                    ship.Hit(_r.Next(1, 10), gameBoard.Hud);                    
                }
                else
                {
                    Point move = this.Slot.GBPos;

                    if (ship.Slot.GBPos.X < this.Slot.GBPos.X)
                        move.X--;
                    else if (ship.Slot.GBPos.X > this.Slot.GBPos.X)
                        move.X++;

                    if (ship.Slot.GBPos.Y < this.Slot.GBPos.Y)
                        move.Y--;
                    else if (ship.Slot.GBPos.Y > this.Slot.GBPos.Y)
                        move.Y++;

                    this.TurnOver = false;
                    gameBoard.MovePieceTo(move, this, () => { this.TurnOver = true; });
                }
            }
            else
            {
                Point moveTo = this.Slot.GBPos;
                var mx = _r.Next(-1, 2);
                moveTo.X += mx;
                if (mx == 0)
                    moveTo.Y += _r.Next(-1, 2);

                if (moveTo == this.Slot.GBPos)
                    return;

                this.TurnOver = false;
                gameBoard.MovePieceTo(moveTo, this, () => { this.TurnOver = true; });
            }
        }
    }
}
