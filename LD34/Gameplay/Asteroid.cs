﻿using Coldsteel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class Asteroid : BaseGamePiece
    {
        private int _metalsLevel = 0;

        private Random _r;

        private SoundEffect _exp;

        public Asteroid(Texture2D texture, Random r, SoundEffect _exp)
        {
            this.Renderer = new SpriteRenderer(this, texture);
            _metalsLevel = r.Next(0, 100);
            this._r = r;
            this._exp = _exp;
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
            this.Interact(gameBoard, ship);
        }

        public override void Interact(GameBoard gameBoard, Ship ship)
        {
            ship.UseFuel(_r.Next(1, 10), gameBoard.Hud);
            ship.GiveMetals(this._metalsLevel, gameBoard.Hud);
            gameBoard.DestroyPeice(this);
            this._exp.Play();
            gameBoard.Hud.Log("The asteroid is no more");
        }
    }
}
