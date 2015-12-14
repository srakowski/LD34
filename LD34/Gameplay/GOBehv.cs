using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LD34.Stages;

namespace LD34.Gameplay
{
    class GOBehv : Behavior
    {
        private GameProgressManager _progress;

        private bool ended = false;

        public GOBehv(GameObject obj, GameProgressManager progress) : base(obj)
        {
            this._progress = progress;
        }

        public override void Update(GameTime gameTime)
        {
            if (_progress.IsGameOver() && !ended)
            {
                LoadingScreen.LoadScreen(this.ScreenManager, new GameStageScreen(new DefeatStage()));
                ended = true;
            }
        }
    }
}
