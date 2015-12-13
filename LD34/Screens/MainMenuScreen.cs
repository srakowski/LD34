using Coldsteel;
using Coldsteel.UI;
using LD34.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD34.Screens
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
            : base("LD34", "SpriteFonts/MenuSpriteFont")
        {
            var play = new MenuEntry("Play");
            play.Selected += Play_Selected;
            this.MenuEntries.Add(play);

            var exit = new MenuEntry("Exit");
            exit.Selected += Exit_Selected;
            this.MenuEntries.Add(exit);
        }

        private void Play_Selected(object sender, PlayerIndexEventArgs e)
        {
        }

        private void Exit_Selected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Exit();            
        }
    }
}
