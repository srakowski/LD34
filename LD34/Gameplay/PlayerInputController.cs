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

        private bool _secondaryPerformed = false;

        private bool _prevLeftDown = false;
        private bool _leftDown = false;        

        private bool _prevRightDown = false;
        private bool _rightDown = false;

        public PlayerInputController(GameObject obj, GameBoard gameBoard) : base(obj)
        {
            this._gameBoard = gameBoard;
        }

        public override void HandleInput(InputState input)
        {
            if (_gameBoard.TurnState != TurnState.PLAYER)
                return;

            _prevLeftDown = _leftDown;
            _leftDown = input.KeyDown(Keys.Left) || input.KeyDown(Keys.A);
            _prevRightDown = _rightDown;
            _rightDown = input.KeyDown(Keys.Right) || input.KeyDown(Keys.D);

            if (_leftDown && _prevLeftDown && _prevRightDown && !_rightDown)
            {
                _gameBoard.ActionBoth(Direction.LEFT);
                _secondaryPerformed = true;
                return;
            }

            if (_rightDown && _prevRightDown && _prevLeftDown && !_leftDown)
            {
                _gameBoard.ActionBoth(Direction.RIGHT);
                _secondaryPerformed = true;
                return;
            }

            if (_prevLeftDown && !_leftDown && !_secondaryPerformed)
            {
                _gameBoard.ActionLeft();
                return;
            }
            else if (_prevLeftDown && !_leftDown && _secondaryPerformed)
            {
                _secondaryPerformed = false;
                return;
            }
          
            if (_prevRightDown && !_rightDown && !_secondaryPerformed)
            {
                _gameBoard.ActionRight();
                return;
            }
            else if (_prevRightDown && !_rightDown && _secondaryPerformed)
            {
                _secondaryPerformed = false;
                return;
            }
        }

    }
}
