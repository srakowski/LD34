using Coldsteel;
using LD34.Gameplay;
using LD34.Screens;
using LD34.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LD34
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LD34Game : Game
    {
        GraphicsDeviceManager _graphics;
        ScreenManager _screenManager;

        public LD34Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _screenManager = new ScreenManager(this, _graphics);
            _screenManager.EnableConsole("SpriteFonts/ConsoleSpriteFont");
            //_screenManager.EnableMouseCursor("Sprites/cursor");

            _screenManager.BackgroundColor = Color.Black;
            _screenManager.AddScreen(new GameStageScreen(new TitleStage()));
            this.Components.Add(_screenManager);

            Content.RootDirectory = "Content";
        }

#if WINDOWS || LINUX
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new LD34Game())
                game.Run();
        }
#endif
    }
}
