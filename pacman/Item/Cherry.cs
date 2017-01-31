using Microsoft.Xna.Framework;

namespace Pacman
{
    class Cherry : Item
    {
        #region Constructors
        public Cherry(Vector2 aPosition)
            :base("Cherry", aPosition)
        {
        }
        #endregion

        #region Protected methods
        protected override void PickedUp(Player aPlayer)
        {
            aPlayer.ActivatePowerUp(ItemType.Cherry);
            base.PickedUp(aPlayer);
        }
        #endregion
    }
}
