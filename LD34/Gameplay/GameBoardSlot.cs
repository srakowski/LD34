using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class GameBoardSlot
    {
        public IGamePiece occupant;
        public Vector2 Pos { get; private set; }
        public Point GBPos { get; private set; }

        public GameBoardSlot(Point posInBoard, Vector2 posInWorld)
        {
            occupant = null;
            Pos = posInWorld;
            GBPos = posInBoard;
        }

        public void RotatePos(Vector2 origin, Matrix rotationMatrix)
        {
            Pos = Vector2.Transform(Pos - origin, rotationMatrix) + origin;
        }

        public void SetOccupent(IGamePiece gp, Action completed, bool initial = false)
        {
            gp.Slot?.Clear();
            gp.Slot = this;
            occupant = gp;
            gp.MoveTo((int)Pos.X, (int)Pos.Y, completed, initial);
        }

        public void Clear()
        {
            this.occupant = null;
        }

        internal void UpdateOccupantPos()
        {
            if (this.occupant == null)
                return;

            this.occupant.Transform.Position = this.Pos;
        }
    }
}
