using Microsoft.Xna.Framework;

namespace Pacman
{
    abstract class Tile : Entity
    {
        #region Member variables
        #endregion

        #region Properties
        static public int Size
        {
            get { return 32; }
        }

        public bool CanWalkOn
        {
            get;
            protected set;
        }

        public int Column
        {
            get;
            private set;
        }

        public int Row
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public Tile(string aFileName, int aColumn, int aRow)
            : base(aFileName, new Vector2(aColumn * Size, aRow * Size))
        {
            InitializePrpoerties(aColumn, aRow);
        }
        #endregion

        #region Public methods
        virtual public void Update(Player aPlayer, GameTime aGameTime)
        {
        }

        virtual public void Reset()
        {
        }
        #endregion

        #region Private methods
        private void InitializePrpoerties(int aColumn, int aRow)
        {
            Column = aColumn;
            Row = aRow;
        }
        #endregion
    }
}
