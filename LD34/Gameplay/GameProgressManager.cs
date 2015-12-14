using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    public class GameProgressManager
    {
        public const int MAX_LEVEL = 42;

        public int Level { get; set; } = 0;

        public ShipState ShipState { get; set; } = new ShipState();

        public bool IsGameOver()
        {
            return this.ShipState.CheckIsDestroyed();
        }
    }
}
