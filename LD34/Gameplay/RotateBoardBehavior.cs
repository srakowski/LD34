using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LD34.Gameplay
{
    class RotateBoardBehavior : Behavior
    {
        private List<LineRenderer> _lineRenderers;

        private GameBoardSlot[,] _slots;

        private Vector2 _origin;

        private int _dir;

        private int _val;

        private StarFieldRenderer _sfr;

        public RotateBoardBehavior(GameObject obj, Vector2 origin,
            List<LineRenderer> lineRenderers, GameBoardSlot[,] slots,
            StarFieldRenderer sfr, int dir) 
            : base(obj)
        {
            _origin = origin;
            _lineRenderers = lineRenderers;
            _slots = slots;
            _dir = dir;
            _val = 0;
            _sfr = sfr;
        }

        public override void Update(GameTime gameTime)
        {
            _val += _dir;

            foreach (var r in _lineRenderers)
                r.Rotate(_origin,
                    Matrix.Identity * Matrix.CreateRotationZ(MathHelper.ToRadians(_dir)));

            foreach (var s in _slots)
                s.occupant?.Transform.Rotate(_origin, Matrix.Identity * Matrix.CreateRotationZ(MathHelper.ToRadians(_dir)));

            _sfr.RotateStars(new Vector2(this.GameObject.ScreenManager.GraphicsDevice.Viewport.Width * 0.5f,
                this.GameObject.ScreenManager.GraphicsDevice.Viewport.Height * 0.5f), 
                Matrix.Identity * Matrix.CreateRotationZ(MathHelper.ToRadians(_dir)));

            if (Math.Abs(_val) == 90)
                this.Behaviors.Remove(this);
        }
    }
}
