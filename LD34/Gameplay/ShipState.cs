using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    public class ShipState
    {
        private const int MAX_FUEL = 300;
        private const int MAX_HULL = 200;
        private const int MAX_ORGANICS = 200;
        private const int MAX_METALS = 3000;

        public int FuelUnits { get; set; } = 100;
        public void ConsumeFuel(int amount = 1)
        {
            this.FuelUnits -= amount;
            if (this.FuelUnits < 0)
                this.FuelUnits = 0;
        }

        public int HullUnits { get; set; } = 100;
        public void Hit(int dmg)
        {
            this.HullUnits -= dmg;
            if (this.HullUnits < 0)
                this.HullUnits = 0;
        }

        public int MetalFragments { get; set; } = 0;

        public int Organics { get; set; } = 0;

        public void ProcessRaw(HudConsole hud)
        {
            if (MetalFragments > 0)
            {
                var add = (MetalFragments / 30);
                HullUnits += add;
                hud.Log(MetalFragments.ToString() + " metal fragments restored " + add.ToString() + " units to your hull");
                MetalFragments = 0;
            }
            
            if (Organics > 0)
            {
                var add = (Organics / 2);
                FuelUnits += add;
                hud.Log(Organics.ToString() + " organic material yielded " + add.ToString() + " additional fuel");
                Organics = 0;
            }
        }

        public bool CheckIsDestroyed()
        {
            return this.FuelUnits == 0 ||
                this.HullUnits == 0;
        }
    }
}
