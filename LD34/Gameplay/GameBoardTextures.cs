using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace LD34.Gameplay
{
    class GameBoardTextures
    {
        public Texture2D SelecterTexture { get; set; }
        public Texture2D AsteroidTexture { get; set; }
        public Texture2D PlanetTexture { get; set; }
        public Texture2D PeaceKeeperShip1 { get; set; }
        public Texture2D JumpGate { get; set; }
        public Texture2D TradeStation { get; set; }
        public Texture2D Pirate { get; set; }
        public SoundEffect _expload;
        public SoundEffect _convert;
        public SoundEffect _jump;
        public SoundEffect _shipHit;
        public void LoadContent(ContentManager content)
        {
            this.SelecterTexture = content.Load<Texture2D>("Sprites/select");
            this.AsteroidTexture = content.Load<Texture2D>("Sprites/asteroid");
            this.PlanetTexture = content.Load<Texture2D>("Sprites/planet");
            this.PeaceKeeperShip1 = content.Load<Texture2D>("Sprites/pk_ship1");
            this.JumpGate = content.Load<Texture2D>("Sprites/jumpgate");
            this.TradeStation = content.Load<Texture2D>("Sprites/station");
            this.Pirate = content.Load<Texture2D>("Sprites/pirate");

            this._expload = content.Load<SoundEffect>("Audio/expload");
            this._convert = content.Load<SoundEffect>("Audio/convert");
            this._jump = content.Load<SoundEffect>("Audio/jump");
            this._shipHit = content.Load<SoundEffect>("Audio/shiphit");
        }
    }
}
