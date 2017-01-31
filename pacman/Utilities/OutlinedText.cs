using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utilities
{
    public static class OutlinedText
    {
        #region Public methods
        public static void DrawWidthCenteredText(SpriteBatch aSpriteBatch, SpriteFont aFont, float aScale,
            string aText, float aYPosition, Color? aColor = null)
        {
            Vector2 textSize = aFont.MeasureString(aText);
            Vector2 position = new Vector2(WindowManager.WindowWidth / 2 - (textSize.X / 2 * aScale), aYPosition - (textSize.Y / 2 * aScale));
            DrawOutlinedText(aSpriteBatch, aFont, aText, position, aScale, aColor ?? Color.White);
        }

        public static void DrawHeightCenteredText(SpriteBatch aSpriteBatch, SpriteFont aFont, float aScale,
    string aText, float aXPosition, Color? aColor = null)
        {
            Vector2 textSize = aFont.MeasureString(aText);
            Vector2 position = new Vector2(aXPosition - (textSize.X / 2 * aScale), WindowManager.WindowHeight / 2 - (textSize.Y / 2 * aScale));
            DrawOutlinedText(aSpriteBatch, aFont, aText, position, aScale, aColor ?? Color.White);
        }

        public static void DrawCenteredText(SpriteBatch aSpriteBatch, SpriteFont aFont, float aScale,
            string aText, Vector2 aPosition, Color? aColor = null)
        {
            Vector2 textSize = aFont.MeasureString(aText);
            Vector2 position = new Vector2(aPosition.X - (textSize.X / 2 * aScale), aPosition.Y - (textSize.Y / 2 * aScale));
            DrawOutlinedText(aSpriteBatch, aFont, aText, position, aScale, aColor ?? Color.White);
        }


        public static void DrawOutlinedText(SpriteBatch aSpritebatch, SpriteFont aFont,
            string aString, Vector2 aPosition, float aScale, Color aColor)
        {
            TextShadow(aSpritebatch, aFont, aString, aPosition, aScale);

            aSpritebatch.DrawString(aFont, aString, aPosition,
                   aColor, 0, Vector2.Zero, aScale, SpriteEffects.None, 1);
        }
        #endregion

        #region Private methods
        private static void TextShadow(SpriteBatch aSpritebatch,
            SpriteFont aFont, string aString, Vector2 aPosition, float aScale)
        //Renders out 4 texts slightly off from each other appearing like a black border.
        {
            aSpritebatch.DrawString(aFont, aString, new Vector2(aPosition.X - 2, aPosition.Y),
                 Color.Black, 0, Vector2.Zero, aScale, SpriteEffects.None, 1);

            aSpritebatch.DrawString(aFont, aString, new Vector2(aPosition.X + 2, aPosition.Y),
                 Color.Black, 0, Vector2.Zero, aScale, SpriteEffects.None, 1);

            aSpritebatch.DrawString(aFont, aString, new Vector2(aPosition.X, aPosition.Y - 2),
                 Color.Black, 0, Vector2.Zero, aScale, SpriteEffects.None, 1);

            aSpritebatch.DrawString(aFont, aString, new Vector2(aPosition.X - 2, aPosition.Y + 2),
                 Color.Black, 0, Vector2.Zero, aScale, SpriteEffects.None, 1);
        }
        #endregion
    }
}
