using Coldsteel;
using LD34.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class PlayerSelectBehavior : Behavior
    {
        public PlayerSelectBehavior(GameObject obj) : base(obj)
        {
        }

        public override void HandleInput(InputState input)
        {
            PlayerIndex pIdx;
            if (input.IsMenuSelect(null, out pIdx))
                LoadingScreen.LoadScreen(this.ScreenManager,
                    new GameStageScreen(new GameplayStage(new GameProgressManager())));
        }
    }
}
