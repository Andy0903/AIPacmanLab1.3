using Microsoft.Xna.Framework;

namespace Pacman
{
    class ItemFactory
    {
        #region Constructors
        public ItemFactory()
        {
        }
        #endregion

        #region Public methods
        public Item GetItem(ItemType aItemType, Vector2 aPosition)
        {
            switch (aItemType)
            {
                case ItemType.Pellet:
                   return new Pellet(aPosition);
                case ItemType.Cherry:
                    return new Cherry(aPosition);
                case ItemType.Key:
                    return new Key(aPosition);
                case ItemType.Melon:
                    return new Melon(aPosition);
            }
            return null;
        }

        #endregion
    }
}
