using Coldsteel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    interface IGamePiece
    {
        GameBoardSlot Slot { get; set; }
        Transform Transform { get; }
        void MoveTo(int x, int y, Action completed, bool instant = false);
        void Select(HudConsole hud, GameBoard gameBoard, Ship ship);
        bool IsSelected { get; }
        void Interact(GameBoard gameBoard, Ship ship);
        bool TurnOver { get; set; }
        bool IsTraversable { get; }
        void BeginTurn(GameTime gameTime, GameBoard gameBoard, Ship ship);
    }
}
