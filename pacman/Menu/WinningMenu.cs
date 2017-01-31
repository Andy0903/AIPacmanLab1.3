using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utilities;

namespace Pacman
{
    class WinningMenu : GameOverMenu
    {
        #region Constructors
        public WinningMenu(SpriteFont aFont): 
        base(aFont)
        {
        }
        #endregion

        #region protected methods
        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, "Congratulations! You won!", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Score: " + GameBoard.Score, WindowManager.WindowHeight - WindowManager.WindowHeight / 1.15f, Color.White);
        }
        #endregion
    }
}
