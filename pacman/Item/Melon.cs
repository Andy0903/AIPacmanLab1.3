using Microsoft.Xna.Framework;
namespace Pacman
{
    class Melon : Item
    {
        #region Constructors
        public Melon(Vector2 aPosition)
            :base("Melon", aPosition)
        {
        }
        #endregion

        #region Protected methods
        protected override void PickedUp(Player aPlayer)
        {
            GameBoard.Score += 100;
            base.PickedUp(aPlayer);
        }
        #endregion
    }
}
