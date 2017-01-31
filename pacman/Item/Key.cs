using Microsoft.Xna.Framework;
namespace Pacman
{
    class Key : Item
    {
        #region Constructors
        public Key(Vector2 aPosition)
            :base("Key", aPosition)
        {
        }
        #endregion

        #region Protected methods
        protected override void PickedUp(Player aPlayer)
        {
            aPlayer.ActivatePowerUp(ItemType.Key);
            base.PickedUp(aPlayer);
        }
        #endregion
    }
}
