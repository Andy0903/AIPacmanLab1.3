using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Utilities;

namespace Pacman
{
    class GameOverMenu : Menu
    {
        #region Constructors
        public GameOverMenu(SpriteFont aFont):
            base(aFont)
        {
        }

        #endregion

        #region Events

        public event EventHandler MenuSelected;

        public event EventHandler RestartSelected;

        #endregion

        #region Protected methods
        override protected void KeyPressing()
        {
            if (KeyboardUtility.WasClicked(Keys.Enter) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.A))
            {
                RestartSelected(this, EventArgs.Empty);
            }

            if (KeyboardUtility.WasClicked(Keys.Escape) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.B))
            {
                MenuSelected(this, EventArgs.Empty);
            }
        }

        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, "Game Over", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Score: " + GameBoard.Score, WindowManager.WindowHeight - WindowManager.WindowHeight / 1.15f, Color.White);
        }

        override protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press A to restart", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Yellow);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press B to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Yellow);
        }

        override protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ENTER to restart", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ESCAPE to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Red);
        }
        #endregion
    }
}
