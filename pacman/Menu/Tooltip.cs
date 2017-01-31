using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Tooltip : Entity
    {
        #region Member variables
        SpriteFont myFont;
        string myText;
        Direction myDirection;
        int mySlot;
        #endregion

        #region Constructors
        public Tooltip(Vector2 aPosition, Direction aDirection, int aSlot)
            : base("Tooltip", aPosition)
        {
            myFont = Game1.myContentManager.Load<SpriteFont>("TooltipFont");
            myDirection = aDirection;
            mySlot = aSlot;
            myText = "";
        }
        #endregion

        #region Public methods
        public override void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            base.Draw(aSpriteBatch, aColor);
            DrawText(aSpriteBatch);
        }

        public void Update()
        {
            myText = PlayerInput.GetKeyText(myDirection, mySlot);
        }
        #endregion

        #region Private methods
        private void DrawText(SpriteBatch aSpriteBatch)
        {
            Utilities.OutlinedText.DrawCenteredText(aSpriteBatch, myFont, 1, myText, Center, Color.White);
        }
        #endregion
    }
}
