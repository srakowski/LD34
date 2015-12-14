using Coldsteel;
using LD34.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Stages
{
    class TitleStage : GameStage
    {
        Texture2D _fontTexture;

        public override void LoadContent(ContentManager content)
        {
            _fontTexture = content.Load<Texture2D>("Sprites/font");
        }

        public override void Initialize()
        {
            ScreenManager.BackgroundColor = new Color(23, 23, 33);
            var hc = new HudConsole(_fontTexture);
            this.AddGameObject(hc);
            hc.InitializeHud();
            hc.Transform.Position += new Vector2(300, 280);
            hc.Log("Adrift: A Space Age Roguelite");
            hc.Log("Created by Shawn Rakowski");
            hc.Log("For Ludum Dare 34");

            var pe = new GameObject();
            var tr = new TextRenderer(pe, _fontTexture);
            pe.Renderer = tr;
            tr.Text = "Press [Enter] to play...";
            tr.Throb = true;
            pe.Transform.Position += new Vector2(300, 360);
            this.AddGameObject(pe);

            this.Behaviors.Add(new PlayerSelectBehavior(this.RootGameObject));
        }
    }
}
