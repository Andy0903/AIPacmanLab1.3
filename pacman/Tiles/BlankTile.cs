using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class BlankTile : Tile
    {
        #region Member variables
        Item myItem;
        Item myItemHadFromStart;
        #endregion

        #region Properties
        public bool HasPellet
        {
            get { return myItem is Pellet ? true : false; }
        }

        public bool HasItem
        {
            get { return myItem != null ? true : false; }
        }
        #endregion

        #region Constructors
        public BlankTile(int aGridX, int aGridY)
            : base("DarkBlueTile", aGridX, aGridY)
        {
            InitializeMemberVariables();
            InitializeProperties();
        }
        #endregion

        #region Public methods
        public override void Update(Player aPlayer, GameTime aGameTime)
        {
            if (myItem != null && Collision(aPlayer))
            {
                myItem.Update(aPlayer);
                RemoveItem();
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            base.Draw(aSpriteBatch, aColor);
            if (myItem != null)
            {
                myItem.Draw(aSpriteBatch, aColor);
            }
        }

        public void AddItem(Item aItem)
        {
            myItem = aItem;
            myItemHadFromStart = aItem;
        }

        public void AddTemporaryItem(Item aItem)
        {
            myItem = aItem;
        }

        public void RemoveItem()
        {
            myItem = null;
        }

        override public void Reset()
        {
            myItem = myItemHadFromStart;
        }
        #endregion

        #region Private methods
        private bool Collision(Player aPlayer)
        {
            if (aPlayer.SizeHitbox.Intersects(myItem.Hitbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitializeMemberVariables()
        {
            myItem = null;
            myItemHadFromStart = null;
        }

        private void InitializeProperties()
        {
            CanWalkOn = true;
        }
        #endregion
    }
}
