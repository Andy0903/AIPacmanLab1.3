using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class WallTile : Tile
    {
        #region Member variables
        Rectangle mySourceRectangle;
        Texture2D myBrokenTexture;

        WallTileNeighbors myWallTileNeighbors;
        #endregion

        #region Properties
        public bool Broken
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public WallTile(int aGridX, int aGridY)
            : base("Tileset", aGridX, aGridY)
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Public methods
        public override void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            if (Broken == false)
            {
                aSpriteBatch.Draw(Texture, Position, mySourceRectangle, aColor ?? Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            else
            {
                aSpriteBatch.Draw(myBrokenTexture, Position, aColor ?? Color.White);
            }
        }

        public void RemoveNeighbor(WallTileNeighbors aWallTileNeighbors)
        {
            myWallTileNeighbors &= ~aWallTileNeighbors;
            //~ gives the compliment of what I'm about to remove. Want to remove will be 0, all others will be 1.
            //Therefore only the unwanted neighbors will be removed.
            UpdateSourceRectangle();
        }

        public void AddNeighbor(WallTileNeighbors aWallTileNeighbors)
        {
            myWallTileNeighbors |= aWallTileNeighbors;
            UpdateSourceRectangle();
        }

        override public void Update(Player aPlayer, GameTime aGameTime)
        {
            if (Broken == true)
            {
                CanWalkOn = true;
            }
        }

        override public void Reset()
        {
            Broken = false;
            CanWalkOn = false;
        }
        #endregion

        #region Private methods

        private void UpdateSourceRectangle()
        {
            switch (myWallTileNeighbors)
            {
                case WallTileNeighbors.Empty:
                    mySourceRectangle = new Rectangle(Size * 0, Size * 0, Size, Size);
                    break;


                case WallTileNeighbors.North:
                    mySourceRectangle = new Rectangle(Size * 2 + 2, Size * 0, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.South:
                    mySourceRectangle = new Rectangle(Size * 2 + 2, Size * 2 + 2, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.South | WallTileNeighbors.East:
                    mySourceRectangle = new Rectangle(Size * 3 + 3, Size * 2 + 2, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.South | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 2 + 2, Size * 3 + 3, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.South | WallTileNeighbors.East | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 3 + 3, Size * 3 + 3, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.East:
                    mySourceRectangle = new Rectangle(Size * 3 + 3, Size * 0, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.East | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 3 + 3, Size * 1 + 1, Size, Size);
                    break;
                case WallTileNeighbors.North | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 2 + 2, Size * 1 + 1, Size, Size);
                    break;


                case WallTileNeighbors.South:
                    mySourceRectangle = new Rectangle(Size * 0, Size * 2 + 2, Size, Size);
                    break;
                case WallTileNeighbors.South | WallTileNeighbors.East:
                    mySourceRectangle = new Rectangle(Size * 1 + 1, Size * 2 + 2, Size, Size);
                    break;
                case WallTileNeighbors.South | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 0, Size * 3 + 3, Size, Size);
                    break;
                case WallTileNeighbors.South | WallTileNeighbors.East | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 1 + 1, Size * 3 + 3, Size, Size);
                    break;

                case WallTileNeighbors.East:
                    mySourceRectangle = new Rectangle(Size * 1 + 1, Size * 0, Size, Size);
                    break;
                case WallTileNeighbors.East | WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 1 + 1, Size * 1 + 1, Size, Size);
                    break;


                case WallTileNeighbors.West:
                    mySourceRectangle = new Rectangle(Size * 0, Size * 1 + 1, Size, Size);
                    break;
            }
        }

        private void InitializeMemberVariables()
        {
            myBrokenTexture = Game1.myContentManager.Load<Texture2D>("DarkBlueTile");
            mySourceRectangle = new Rectangle(0, 0, Size, Size);
            myWallTileNeighbors = WallTileNeighbors.Empty;
            CanWalkOn = false;
            Broken = false;
        }
        #endregion
    }
}
