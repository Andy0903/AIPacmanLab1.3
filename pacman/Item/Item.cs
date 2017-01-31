using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    abstract class Item : Entity
    {
        #region Constructors
        public Item(string aFileName, Vector2 aPosition)
            : base(aFileName, aPosition)
        {
        }
        #endregion

        #region Public methods
        public void Update(Player aPlayer)
        {
            PickedUp(aPlayer);
        }

        override public void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            aSpriteBatch.Draw(Texture, new Vector2
                (Position.X + Tile.Size / 2 - Texture.Width / 2, Position.Y + Tile.Size / 2 - Texture.Height / 2),
                aColor ?? Color.White);
        }
        #endregion

        #region Protected methods
        virtual protected void PickedUp(Player aPlayer)
        {
            SoundEffectManager.PlayItemSound();
        }
        #endregion

    }
}
