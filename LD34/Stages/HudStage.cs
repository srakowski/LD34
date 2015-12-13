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

        public override void LoadContent(ContentManager content)
        {
            _fontTexture = content.Load<Texture2D>("Sprites/font");
        }

        public override void Initialize()
        {
            this.HudConsole = new HudConsole(_fontTexture);
            this.AddGameObject(this.HudConsole);
            this.HudConsole.InitializeHud();



            //var backDropGO = new GameObject();
            //backDropGO.Renderer = new RectangleRenderer(backDropGO,
            //    ScreenManager.GraphicsDevice,
            //    new Rectangle(0, 0, 400, ScreenManager.GraphicsDevice.Viewport.Height),
            //    new Color(25, 25, 25));
            //this.AddGameObject(backDropGO);

            //var dividerGO = new GameObject();
            //var divRenderer = new LineRenderer(dividerGO, ScreenManager.GraphicsDevice);
            //divRenderer.AddVector(new Vector2(401, 0));
            //divRenderer.AddVector(new Vector2(401, ScreenManager.GraphicsDevice.Viewport.Height));
            //divRenderer.Color = Color.DarkGray;
            //dividerGO.Renderer = divRenderer;
            //this.AddGameObject(dividerGO);





        }
    }
}
