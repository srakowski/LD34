using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD34.Gameplay
{
    class PlayerInputController : Behavior
    {
        private GameBoard _gameBoard;

        public PlayerInputController(GameObject obj, GameBoard gameBoard) : base(obj)
        {
            this._gameBoard = gameBoard;
        }

        public override void HandleInput(InputState input)
        {
            PlayerIndex pIdx;

            if (input.KeyDown(Keys.Left) && input.KeyPressed(Keys.Right, out pIdx))
            {
                _gameBoard.ActionBoth(Direction.LEFT);
            }
            else if (input.KeyDown(Keys.Right) && input.KeyPressed(Keys.Left, out pIdx))
            {
                _gameBoard.ActionBoth(Direction.RIGHT);
            }
            else if (input.KeyPressed(Keys.Left, out pIdx))
            {
                _gameBoard.ActionLeft();
            }
            else if (input.KeyPressed(Keys.Right, out pIdx))
            {
                _gameBoard.ActionRight();
            }
        }
    }
}
