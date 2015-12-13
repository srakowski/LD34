using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coldsteel;
using Microsoft.Xna.Framework.Graphics;

namespace LD34.Gameplay
{
    class Ship : Behavior, IGamePiece
    {
        public Bearing Bearing { get; set; }

        public Ship(GameObject ship)
            : base(ship)
        {
            this.Bearing = Bearing.NORTH;
        }

        public void MoveTo(int x, int y, bool instant = false)
        {
            if (!instant)
                this.Behaviors.Add(new MoveToBehavior(this.GameObject, new Microsoft.Xna.Framework.Vector2(x, y), 0.1f));
            else
                this.Transform.Position = new Microsoft.Xna.Framework.Vector2(x, y);
        }

        public void MoveLeft()
        {
            this.Behaviors.Add(new MoveToBehavior(this.GameObject,
                this.Transform.Position + new Microsoft.Xna.Framework.Vector2(-48, -48), 0.1f));
        }

        public void MoveRight()
        {
            this.Behaviors.Add(new MoveToBehavior(this.GameObject,
                this.Transform.Position + new Microsoft.Xna.Framework.Vector2(48, -48), 0.1f));
        }

        //public void ActionLeft()
        //{
        //    this.Behaviors.Add(new MoveToBehavior(this.GameObject, 
        //        this.Transform.Position + new Microsoft.Xna.Framework.Vector2(-48, -48), 0.1f));
        //}

        //public void ActionRight()
        //{

        //}
    }
}
