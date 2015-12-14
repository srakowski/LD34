using Coldsteel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class BaseGamePiece : GameObject, IGamePiece
    {
        public GameBoardSlot Slot { get; set; } = null;

        public virtual bool IsTraversable { get { return false; } }

        public bool IsSelected { get; set; } = false;

        public bool TurnOver { get; set; } = true;

        public virtual void MoveTo(int x, int y, Action completed, bool instant = false)
        {
            if (!instant)
            {
                this.Behaviors.Add(new MoveToBehavior(this, new Microsoft.Xna.Framework.Vector2(x, y), 0.4f)
                {
                    OnMoveCompleted = new Action(() =>
                    {
                        this.Slot.UpdateOccupantPos();
                        completed?.Invoke();
                    })
                });
            }
            else
            {
                this.Transform.Position = new Microsoft.Xna.Framework.Vector2(x, y);
                this.Slot.UpdateOccupantPos();
                completed?.Invoke();
            }
        }

        public virtual void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
        }

        public virtual void Interact(GameBoard gameBoard, Ship ship)
        {
        }

        public virtual void BeginTurn(GameTime gameTime, GameBoard gameBoard, Ship ship)
        {
        }

        public bool IsAdjacentTo(Point pos)
        {
            return pos == (this.Slot.GBPos + new Point(-1, 0)) ||
                pos == (this.Slot.GBPos + new Point(1, 0)) ||
                pos == (this.Slot.GBPos + new Point(0, -1)) ||
                pos == (this.Slot.GBPos + new Point(0, 1));
        }
    }
}
