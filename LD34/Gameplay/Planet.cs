using Coldsteel;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class Planet : BaseGamePiece
    {
        private Random _r;

        private bool _hasOrganics = false;

        public Planet(Texture2D texture, Random r)
        {
            this.Renderer = new SpriteRenderer(this, texture);
            this._r = r;
            this._hasOrganics = r.Next(0, 100) < 50;
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
            this.Interact(gameBoard, ship);
        }

        public override void Interact(GameBoard gameBoard, Ship ship)
        {
            ship.UseFuel(_r.Next(1, 20), gameBoard.Hud);

            if (_hasOrganics)           
                ship.GiveOrganics(this._r.Next(0, 40), gameBoard.Hud);

            ship.GiveMetals(this._r.Next(0, 60), gameBoard.Hud);            
        }
    }
}
