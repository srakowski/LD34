using Coldsteel;
using LD34.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Gameplay
{
    class FinalPlanet : BaseGamePiece
    {
        public FinalPlanet(Texture2D texture)
        {
            this.Renderer = new SpriteRenderer(this, texture)
            {
                Color = Color.Gold
            };
        }

        public override void Select(HudConsole hud, GameBoard gameBoard, Ship ship)
        {
            this.Interact(gameBoard, ship);
        }

        public override void Interact(GameBoard gameBoard, Ship ship)
        {
            LoadingScreen.LoadScreen(this.ScreenManager, new GameStageScreen(new VictoryStage()));
        }
    }
}
