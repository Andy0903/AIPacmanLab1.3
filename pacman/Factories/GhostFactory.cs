using Microsoft.Xna.Framework;

namespace Pacman
{
    class GhostFactory
    {
        #region Constructors
        public GhostFactory()
        {
        }
        #endregion

        #region Public methods
        public Ghost GetGhost(GhostType aGhostType, Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
        {
            switch (aGhostType)
            {
                case GhostType.Red:
                    return new RedGhost(aPlayer, aGameBoard, aPosition);
                case GhostType.Blue:
                    return new BlueGhost(aPlayer, aGameBoard, aPosition);
                case GhostType.Violet:
                    return new VioletGhost(aPlayer, aGameBoard, aPosition);
                case GhostType.Orange:
                    return new OrangeGhost(aPlayer, aGameBoard, aPosition);
            }
            return null;
        }

        #endregion
    }
}
