using Coldsteel;
using Microsoft.Xna.Framework;

namespace LD34.Gameplay
{
    internal class StarFieldShifter : Behavior
    {
        private Camera _camera;

        private Vector2 _prevCamPos;        

        private StarFieldRenderer _sfr;

        public StarFieldShifter(GameObject obj, Camera camera, StarFieldRenderer sfr) : base(obj)
        {
            this._camera = camera;
            this._prevCamPos = camera.Position;
            this._sfr = sfr;
        }

        public override void Update(GameTime gameTime)
        {
            if (_camera.Position == _prevCamPos)
                return;

            _sfr.ShiftStars(_prevCamPos - _camera.Position);
            _prevCamPos = _camera.Position;
        }
    }
}