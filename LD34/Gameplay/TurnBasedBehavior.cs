using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LD34.Gameplay
{
    class TurnBasedBehavior : Behavior
    {
        public bool PlayerTurn { get; private set; } = true;

        public TurnBasedBehavior(GameObject obj) : base(obj)
        {

        }

        public void PlayerDone()
        {
            PlayerTurn = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (PlayerTurn)
                return;
            
            //Game            
        }
    }
}
