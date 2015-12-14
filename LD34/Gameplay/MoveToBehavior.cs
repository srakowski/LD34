using Coldsteel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class MoveToBehavior : Behavior
    {
        public Action OnMoveCompleted { get; set; } = null;

        private Vector2 _moveTo = Vector2.Zero;

        private bool _rotate;

        public MoveToBehavior(GameObject obj, Vector2 moveTo, float speed, bool rotate = true) : base(obj)
        {
            _moveTo = moveTo;
            var direction = this._moveTo - this.Transform.Position;
            direction.Normalize();
            this.RigidBody.velocity = direction * speed;
            this._rotate = rotate;
            if (this._rotate)
                this.Transform.Rotation = (float)Math.Atan2(direction.X, -direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (Vector2.Distance(this.Transform.Position, _moveTo) > this.RigidBody.velocity.Length() + 10)
                return;

            this.Transform.Position = _moveTo;
            this.RigidBody.velocity = Vector2.Zero;
            this.Behaviors.Remove(this);
            if (this._rotate)
                this.Transform.Rotation = 0f;

            OnMoveCompleted?.Invoke();
        }
    }
}
