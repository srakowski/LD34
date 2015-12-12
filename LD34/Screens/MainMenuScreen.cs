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
            : base("LD341", "MenuSpriteFont")
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
            LoadingScreen.LoadScreen(this.ScreenManager,
                new GameStageScreen(new GameplayStage()));
        }

        private void Exit_Selected(object sender, PlayerIndexEventArgs e)
        {
        }
    }
}
