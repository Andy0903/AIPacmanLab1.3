using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Utilities;

namespace Pacman
{
    class MainMenu : Menu
    {
        #region Constructors
        public MainMenu(SpriteFont aFont)
            :base(aFont)
        {
        }

        #endregion

        #region Events
        public event EventHandler StartSelected;

        public event EventHandler HighscoreSelected;

        public event EventHandler OptionsSelected;

        public event EventHandler ExitSelected;

        #endregion

        #region Protected methods
        override protected void KeyPressing()
        {
            if (KeyboardUtility.WasClicked(Keys.Enter) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.A))
            {
                StartSelected(this, EventArgs.Empty);
            }
            else if (KeyboardUtility.WasClicked(Keys.Space) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.Y))
            {
                HighscoreSelected(this, EventArgs.Empty);
            }
            else if (KeyboardUtility.WasClicked(Keys.O)) //EJ stöd av XboxControll.
            {
                OptionsSelected(this, EventArgs.Empty);
            }
            else if (KeyboardUtility.WasClicked(Keys.Escape) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.B))
            {
                ExitSelected(this, EventArgs.Empty);
            }
        }

        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, "Pacman", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.2f, "by Andreas Gustafsson", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.15f);
        }

        override protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press A to start", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Yellow);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press Y for highscores", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.5f, Color.Yellow);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press B to exit", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Yellow);
        }

        override protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ENTER to start", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press SPACE for highscores", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.5f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press O for options", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.6f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ESCAPE to exit", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Red);
        }
        #endregion
    }
}
