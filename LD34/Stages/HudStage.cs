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
    class HudStage : GameStage
    {
        public HudConsole HudConsole { get; set; }

        Texture2D _fontTexture;

        private GameProgressManager _progress;

        public HudStage(GameProgressManager progress) : base()
        {
            _progress = progress;
        }

        public override void LoadContent(ContentManager content)
        {
            _fontTexture = content.Load<Texture2D>("Sprites/font");
        }

        public override void Initialize()
        {
            this.HudConsole = new HudConsole(_fontTexture);
            this.AddGameObject(this.HudConsole);
            this.HudConsole.InitializeHud();

            var fuelStateGO = new GameObject();
            var fsRend = new TextRenderer(fuelStateGO, _fontTexture);
            fuelStateGO.Renderer = fsRend;
            fuelStateGO.Transform.Position += new Vector2(142, ScreenManager.GraphicsDevice.Viewport.Height - 20);
            fuelStateGO.Behaviors.Add(new TextUpdBehavior(fuelStateGO, _progress,
                new Func<GameProgressManager, string>((prog) => "Fuel: " + _progress.ShipState.FuelUnits.ToString() )));
            this.AddGameObject(fuelStateGO);

            var hullStateGO = new GameObject();
            var hullRend = new TextRenderer(hullStateGO, _fontTexture);
            hullStateGO.Renderer = hullRend;
            hullStateGO.Transform.Position += new Vector2(2, ScreenManager.GraphicsDevice.Viewport.Height - 20);
            hullStateGO.Behaviors.Add(new TextUpdBehavior(hullStateGO, _progress,
                new Func<GameProgressManager, string>((prog) => "Hull: " + _progress.ShipState.HullUnits.ToString())));
            this.AddGameObject(hullStateGO);

            var _mStateGO = new GameObject();
            var _mRend = new TextRenderer(_mStateGO, _fontTexture);
            _mStateGO.Renderer = _mRend;
            _mStateGO.Transform.Position += new Vector2(282, ScreenManager.GraphicsDevice.Viewport.Height - 20);
            _mStateGO.Behaviors.Add(new TextUpdBehavior(_mStateGO, _progress,
                new Func<GameProgressManager, string>((prog) => "Metals: " + _progress.ShipState.MetalFragments.ToString())));
            this.AddGameObject(_mStateGO);

            var _organicsGO = new GameObject();
            var _oRend = new TextRenderer(_organicsGO, _fontTexture);
            _organicsGO.Renderer = _oRend;
            _organicsGO.Transform.Position += new Vector2(422, ScreenManager.GraphicsDevice.Viewport.Height - 20);
            _organicsGO.Behaviors.Add(new TextUpdBehavior(_organicsGO, _progress,
                new Func<GameProgressManager, string>((prog) => "Organics: " + _progress.ShipState.Organics.ToString())));
            this.AddGameObject(_organicsGO);
        }
    }
}
