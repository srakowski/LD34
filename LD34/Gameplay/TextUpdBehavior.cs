using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LD34.Gameplay
{
    class TextUpdBehavior : Behavior
    {
        private GameProgressManager _progress;
        private Func<GameProgressManager, string> _updText;

        public TextUpdBehavior(GameObject obj, GameProgressManager progress, Func<GameProgressManager, string> updText) : base(obj)
        {
            this._progress = progress;
            this._updText = updText;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            (this.GameObject.Renderer as TextRenderer).Text = _updText.Invoke(_progress);
        }
    }
}
