using Coldsteel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class Asteroid : GameObject, IGamePiece
    {
        public Asteroid(Texture2D texture)
        {
            this.Renderer = new SpriteRenderer(this, texture);
        }

        public void MoveTo(int x, int y, bool instant = false)
        {
            if (!instant)
                this.Behaviors.Add(new MoveToBehavior(this, new Microsoft.Xna.Framework.Vector2(x, y), 0.1f));
            else
                this.Transform.Position = new Microsoft.Xna.Framework.Vector2(x, y);
        }
    }
}
