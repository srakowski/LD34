using Coldsteel;
using LD34.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LD34.Stages
{
    class StarFieldStage : GameStage
    {
        private Texture2D _starTexture;

        public Camera Camera { get; set; }

        public StarFieldRenderer StarFieldRenderer { get; set; }

        public override void LoadContent(ContentManager content)
        {
            _starTexture = content.Load<Texture2D>("Sprites/star");
        }

        public override void Initialize()
        {
            var starField = new GameObject();
            var sfr = new StarFieldRenderer(starField, _starTexture, Camera,
                ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
            starField.Renderer = sfr;
            this.StarFieldRenderer = sfr;
            var sfs = new StarFieldShifter(starField, Camera, sfr);
            starField.Behaviors.Add(sfs);
            this.AddGameObject(starField);
            this.StarFieldRenderer.InitializeStars(400);
        }
    }
}
