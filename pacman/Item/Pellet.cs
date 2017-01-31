using Microsoft.Xna.Framework;

namespace Pacman
{
    class Pellet : Item
    {
        #region Constructors
        public Pellet(Vector2 aPosition)
            :base("Pellet", aPosition)
        {
        }
        #endregion

        #region Protected methods
        protected override void PickedUp(Player aPlayer)
        {
            GameBoard.Score += 10;
            base.PickedUp(aPlayer);
        }
        #endregion
    }
}
