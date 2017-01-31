using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    abstract class Character : Entity
    {
        #region Member variables
        protected float myFrameTimeCounterMilliseconds;
        protected float myTimePerFrameMilliseconds;
        protected float myElapsedTime;

        protected GameBoard myGameBoard;
        protected Vector2? myTargetPosition;
        protected Vector2 myStartPosition;
        protected Rectangle mySourceRectangle;

        protected readonly int NumberOfXFrames;
        protected readonly int NumberOfYFrames;
        protected readonly int myXPadding;
        protected readonly int myYPadding;
        protected readonly float TotalMovementTimeMilliseconds;

        #endregion

        #region Properties
        public int FrameXIndex { get; set; }
        public int FrameYIndex { get; set; }

        public int Column
        {
            get { return (int)Position.X / Tile.Size; }
        }

        public int Row
        {
            get { return (int)Position.Y / Tile.Size; }
        }

        public Rectangle SizeHitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Size, Size); }
        }

        public Vector2 SpawnPosition
        {
            get;
            private set;
        }

        protected int Size
        {
            get;
            private set;
        }

        protected float Rotation
        {
            get;
            set;
        }

        protected SpriteEffects SpriteEffect
        {
            get;
            set;
        }

        public Direction Direction
        {
            get;
           protected set;
        }

        #endregion

        #region Constructors
        public Character(string aFileName, Vector2 aPosition, int aSize,
            int aNumberOfXFrames, int aNumberOfYFrames, int aXPaddingNumber, int aYPaddingNumber,
            int aTimePerFrameMilliseconds, GameBoard aGameBoard, float aTotalMovementTimeMiliseconds)
            : base(aFileName, aPosition)
        {
            NumberOfXFrames = aNumberOfXFrames;
            NumberOfYFrames = aNumberOfYFrames;
            myXPadding = aXPaddingNumber;
            myYPadding = aYPaddingNumber;
            TotalMovementTimeMilliseconds = aTotalMovementTimeMiliseconds;

            InitializeMemberVariables(aSize, aGameBoard, aTimePerFrameMilliseconds);

        }
        #endregion

        #region Public methods

        override public void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            aSpriteBatch.Draw(Texture, Position + new Vector2((Texture.Width / NumberOfXFrames) / 2, Texture.Height / 2),
                mySourceRectangle, aColor ?? Color.White, Rotation,
                new Vector2((Texture.Width / NumberOfXFrames) / 2, Texture.Height / 2), 1f, SpriteEffect, 0f);
        }

        virtual public void Update(GameTime aGameTime)
        {
            Movement(aGameTime);
            UpdateAnimation(aGameTime);
        }

        virtual public void Reset()
        {
            SetDefaultValuesOnNonParameterMemberVariables();
            SetPosition(SpawnPosition);
        }

        public void SetPosition(Vector2 aPosition)
        {
            myTargetPosition = aPosition;
            myStartPosition = aPosition;
            Position = aPosition;
        }

        public void SetSpawnPosition(Vector2 aPosition)
        {
            SpawnPosition = aPosition;
        }
        #endregion

        #region Protected methods
        virtual protected void Movement(GameTime aGameTime)
        {
            myElapsedTime += aGameTime.ElapsedGameTime.Milliseconds;
            if (myTargetPosition != null)
            {
                if (MovedPastTargetPosition())
                {
                    CorrectPosition();
                    myTargetPosition = NextTarget();

                    myElapsedTime -= TotalMovementTimeMilliseconds;
                }
                else
                {
                    Position = new Vector2(
                        myStartPosition.X + (((Vector2)myTargetPosition).X - myStartPosition.X) * (myElapsedTime / TotalMovementTimeMilliseconds),
                        myStartPosition.Y + (((Vector2)myTargetPosition).Y - myStartPosition.Y) * (myElapsedTime / TotalMovementTimeMilliseconds));
                }
            }
            else
            {
                myTargetPosition = NextTarget();
                myElapsedTime = 0;
            }
        }

        virtual protected Vector2? NextTarget()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return myGameBoard.ValidTargetPosition(Row - 1, Column);
                case Direction.Left:
                    return myGameBoard.ValidTargetPosition(Row, Column - 1);
                case Direction.Down:
                    return myGameBoard.ValidTargetPosition(Row + 1, Column);
                case Direction.Right:
                    return myGameBoard.ValidTargetPosition(Row, Column + 1);
            }
            return null;
        }

        abstract protected void CalculateSourceRectangle(GameTime aGameTime);

        protected void MoveSourceRectangle(GameTime aGameTime)
        {
            CalculateSourceRectangle(aGameTime);

            mySourceRectangle = new Rectangle(
                (Size * FrameXIndex) + (myXPadding * FrameXIndex),
                (Size * FrameYIndex) + (myYPadding * FrameYIndex),
                Size,
                Size);
        }
        #endregion

        #region Private methods

        private void CorrectPosition()
        {
            Position = (Vector2)myTargetPosition;
            myStartPosition = Position;
        }

        private bool MovedPastTargetPosition()
        {
            return myElapsedTime > TotalMovementTimeMilliseconds ? true : false;
        }

        private void UpdateAnimation(GameTime aGameTime)
        {
            MoveSourceRectangle(aGameTime);
        }

        private void InitializeMemberVariables(int aSize, GameBoard aGameBoard, float aTimePerFrameMilliseconds)
        {
            InitializeParameterMemberVariables(aSize, aGameBoard, aTimePerFrameMilliseconds);
            SetDefaultValuesOnNonParameterMemberVariables();
        }

        private void InitializeParameterMemberVariables(int aSize, GameBoard aGameBoard, float aTimePerFrameMilliseconds)
        {
            Size = aSize;
            myGameBoard = aGameBoard;
            myTimePerFrameMilliseconds = aTimePerFrameMilliseconds;
            myFrameTimeCounterMilliseconds = myTimePerFrameMilliseconds;
        }

        private void SetDefaultValuesOnNonParameterMemberVariables()
        {
            FrameXIndex = 0;
            FrameYIndex = 0;
            myElapsedTime = 0;

            Rotation = 0;
            SpriteEffect = SpriteEffects.None;
            myTargetPosition = null;
            Direction = Direction.Left;
        }
        #endregion
    }
}
