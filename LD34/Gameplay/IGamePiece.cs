using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    interface IGamePiece
    {
        Transform Transform { get; }
        void MoveTo(int x, int y, bool instant = false);        
    }
}
