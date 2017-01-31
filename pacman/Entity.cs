using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    abstract class Entity
    {
        #region Member variables
        Texture2D myTexutre;
        Vector2 myPosition;
        #endregion

        #region Properties
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, myTexutre.Width, myTexutre.Height); }
        }

        public Vector2 Position
        {
            get { return myPosition; }
            set { myPosition = value; }
        }

        public Vector2 Center
        {
            get { return new Vector2(Position.X + myTexutre.Width / 2, Position.Y + myTexutre.Height / 2); }
            set { Position = new Vector2(value.X - myTexutre.Width / 2, value.Y - myTexutre.Height / 2); }
        }

        protected Texture2D Texture
        {
            get { return myTexutre; }
        }
        #endregion

        #region Constructors
        protected Entity(string aFileName, Vector2 aPosition)
        {
            InitializeMemberVariables(aFileName, aPosition);
        }
        #endregion

        #region Public methods
        virtual public void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            aSpriteBatch.Draw(myTexutre, myPosition, aColor ?? Color.White);
        }
        #endregion

        #region Private methods
        private void InitializeMemberVariables(string aFileName, Vector2 aPosition)
        {
            if (aFileName != null)
            {
                myTexutre = Game1.myContentManager.Load<Texture2D>(aFileName);
            }
            myPosition = aPosition;
        }
        #endregion
    }
}
