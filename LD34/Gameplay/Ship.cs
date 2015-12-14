using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coldsteel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace LD34.Gameplay
{
    class Ship : Behavior, IGamePiece
    {
        public int MaxDamage { get; internal set; } = 10;

        public GameBoardSlot Slot { get; set; }

        public Bearing Bearing { get; set; }
        
        public Point LeftPoint
        {
            get
            {
                var p = this.Slot.GBPos;
                if (Bearing == Bearing.NORTH)
                    p += new Point(-1, 0);
                else if (Bearing == Bearing.WEST)
                    p += new Point(0, 1);
                else if (Bearing == Bearing.SOUTH)
                    p += new Point(1, 0);
                else if (Bearing == Bearing.EAST)
                    p += new Point(0, -1);
                return p;
            }
        }

        public void ProcessRaw(HudConsole hud, SoundEffect _play)
        {
            if (_state.MetalFragments > 0 || _state.Organics > 0)
                _play.Play();

            this._state.ProcessRaw(hud);
        }

        public void UseFuel(int amount, HudConsole hud)
        {
            hud.Log("Efforts expended " + amount + " fuel");
            this._state.ConsumeFuel(amount);
        }

        public Point RightPoint
        {
            get
            {
                var p = this.Slot.GBPos;
                if (Bearing == Bearing.NORTH)
                    p += new Point(0, -1);
                else if (Bearing == Bearing.WEST)
                    p += new Point(-1, 0);
                else if (Bearing == Bearing.SOUTH)
                    p += new Point(0, 1);
                else if (Bearing == Bearing.EAST)
                    p += new Point(1, 0);
                return p;
            }
        }

        private ShipState _state;

        private SoundEffect _hit;

        private SoundEffect _exp;

        public Ship(GameObject ship, ShipState state, SoundEffect _hit, SoundEffect _exp)
            : base(ship)
        {
            this.Bearing = Bearing.NORTH;
            this._state = state;
            this._hit = _hit;
            this._exp = _exp;
        }

        public void MoveLeft(GameBoard gb, Action completed)
        {
            this.TurnOver = false;
            this._state.ConsumeFuel();
            gb.MovePieceTo(this.LeftPoint, this, () =>
            {
                completed?.Invoke();
                this.TurnOver = true;
            });
        }

        public void MoveRight(GameBoard gb, Action completed)
        {
            this.TurnOver = false;
            this._state.ConsumeFuel();
            gb.MovePieceTo(this.RightPoint, this, () =>
            {
                completed?.Invoke();
                this.TurnOver = true;
            });
        }

        public void Jump()
        {
            this.MoveTo(this.Transform.Position + new Vector2(0, -10000), false, 1f, null);
        }

        public void MoveTo(Vector2 pos, bool instant = false)
        {
            this.MoveTo((int)pos.X, (int)pos.Y, null, instant);
        }

        public void MoveTo(Vector2 pos, bool instant, float speed, Action completed)
        {
            this.MoveTo((int)pos.X, (int)pos.Y, instant, speed, completed);
        }
    
        public void MoveTo(int x, int y, Action completed, bool instant = false)
        {
            MoveTo(x, y, instant, 0.2f, completed);
        }

        public void MoveTo(int x, int y, bool instant, float speed, Action completed)
        {
            if (!instant)
            {
                TurnOver = false;
                this.Behaviors.Add(new MoveToBehavior(this.GameObject, new Microsoft.Xna.Framework.Vector2(x, y), speed)
                {
                    OnMoveCompleted = new Action(() =>
                    {
                        completed?.Invoke();
                        TurnOver = true;
                    })
                });
            }
            else
                this.Transform.Position = new Microsoft.Xna.Framework.Vector2(x, y);
        }

        public bool IsSelected { get; set; } = false;

        public bool TurnOver { get; set; } = true;

        public bool IsTraversable
        {
            get
            {
                return false;
            }
        }

        public void Interact(GameBoard gameBoard, Ship ship) { }

        public void Select(HudConsole hud, GameBoard gameBoard, Ship ship) { }

        public void BeginTurn(GameTime gameTime, GameBoard gameBoard, Ship ship) { }

        public void Hit(int dmg, HudConsole hud)
        {
            _hit.Play();
            hud.Log("You take " + dmg + " points of damage");
            this._state.Hit(dmg);
            if (this._state.CheckIsDestroyed())
                _exp.Play();
        }

        public void BeginRotate()
        {
            this.TurnOver = false;
        }

        public void EndRotate()
        {
            this.TurnOver = true;
        }

        public void GiveMetals(int _metalsLevel, HudConsole hud, bool mining = true)
        {
            this._state.MetalFragments += _metalsLevel;
            if (mining)
                hud.Log("Mining efforts yielded " + _metalsLevel.ToString() + " metal fragments");
            else
                hud.Log("You receive " + _metalsLevel.ToString() + " metal fragments");
        }

        public void GiveFuel(int fuel, HudConsole hud)
        {
            this._state.FuelUnits += fuel;
            hud.Log("You receive " + fuel.ToString() + " fuel");
        }

        public void GiveOrganics(int organics, HudConsole hud)
        {
            this._state.Organics += organics;
            hud.Log("Harvest efforts yielded " + organics.ToString() + " organics");
        }
    }
}
