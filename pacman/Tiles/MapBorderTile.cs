using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class MapBorderTile : Tile
    {
        #region Constructors
        public MapBorderTile(int aGridX, int aGridY)
            : base(null, aGridX, aGridY)
        {
            InitializeProperties();
        }
        #endregion

        #region Public methods
        public override void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
        }
        #endregion

        #region Private methods
        private void InitializeProperties()
        {
            CanWalkOn = false;
        }
        #endregion
    }
}
