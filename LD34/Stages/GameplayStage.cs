using Coldsteel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using LD34.Gameplay;

namespace LD34.Stages
{
    class GameplayStage : GameStage
    {
        private Texture2D _fontTexture;
        private Texture2D _shipTexture;
        private Texture2D _gridTexture;
        private GameBoardTextures _gameBoardTextures;

        public override void LoadContent(ContentManager content)
        {
            _fontTexture = content.Load<Texture2D>("Sprites/font");            
            _shipTexture = content.Load<Texture2D>("Sprites/ship");
            _gridTexture = content.Load<Texture2D>("Sprites/grid");

            _gameBoardTextures = new GameBoardTextures();
            _gameBoardTextures.SelecterTexture = content.Load<Texture2D>("Sprites/select");
            _gameBoardTextures.AsteroidTexture = content.Load<Texture2D>("Sprites/asteroid");            
        }

        public override void Initialize()
        {
            ScreenManager.BackgroundColor = new Color(23, 23, 33);

            var hudStage = new HudStage();
            ScreenManager.AddScreen(new GameStageScreen(hudStage));                        

            var shipGO = new GameObject();
            shipGO.Renderer = new SpriteRenderer(shipGO, _shipTexture);
            shipGO.RigidBody = new RigidBody(shipGO);
            var ship = new Ship(shipGO);
            shipGO.Behaviors.Add(ship);
            this.AddGameObject(shipGO);

            var gameBoardGO = new GameObject();
            var gameBoard = new GameBoard(this.RootGameObject, hudStage.HudConsole, ship, 9, _gameBoardTextures);
            gameBoardGO.Behaviors.Add(gameBoard);
            this.AddGameObject(gameBoardGO);

            this.GameScreen.Camera = new Camera(ScreenManager.GraphicsDevice, shipGO);

            var starFieldStage = new StarFieldStage();
            starFieldStage.Camera = GameScreen.Camera;
            ScreenManager.AddScreen(new GameStageScreen(starFieldStage));
            gameBoard.StarFieldRenderer = starFieldStage.StarFieldRenderer;

            var playerInputController = new PlayerInputController(this.RootGameObject, gameBoard);
            this.Behaviors.Add(playerInputController);

            
            //{
            //    Offset = Matrix.CreateTranslation(200, 0, 0)
            //};

            base.Initialize();
        }
    }
}
