using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utilities;

namespace Pacman
{
    abstract class Menu
    {
        #region Member variables
        protected SpriteFont myFont;
        #endregion

        #region Constructors
        public Menu(SpriteFont aFont)
        {
            InitializeMemberVariables(aFont);
        }

        #endregion
        
        #region Public methods
        public virtual void Update()
        {
            KeyPressing();
        }

        virtual public void Draw(SpriteBatch aSpriteBatch)
        {
            DrawUnchangingText(aSpriteBatch);

            if (XboxControllerUtility.ControllerConnected(PlayerIndex.One))
            {
                DrawXboxControllerInstructions(aSpriteBatch);
            }
            else
            {
                DrawKeyboardInstructions(aSpriteBatch);
            }
        }
        #endregion

        #region Protected methods
        abstract protected void KeyPressing();

        abstract protected void DrawUnchangingText(SpriteBatch aSpriteBatch);

        abstract protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch);

        abstract protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch);
        #endregion

        #region Private methods

        private void InitializeMemberVariables(SpriteFont aFont)
        {
            myFont = aFont;
        }
        #endregion
    }
}
