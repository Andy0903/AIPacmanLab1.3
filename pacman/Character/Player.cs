using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pacman
{
    class Player : Character
    {
        #region Member variables
        float myDurationTimer;
        #endregion

        #region Properties
        public PlayerHealthState PlayerHealthState
        {
            get;
            private set;
        }

        public int Lives
        {
            get;
            private set;
        }

        public PowerUpType PowerUp
        {
            get;
            private set;
        }
        
        #endregion

        #region Constructors
        public Player(GameBoard aGameBoard)
            : base("Pacman", Vector2.Zero, 32, 5, 1, 2, 0, 100, aGameBoard, 200)
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Events
        public event EventHandler GameOver;
        #endregion

        #region Public methods
        override public void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            if (PowerUp != PowerUpType.WallUnlocker)
            {
                base.Draw(aSpriteBatch);
            }
            else
            {
                base.Draw(aSpriteBatch, Color.Lime);
            }
        }

        public void PrepareNewRound()
        {
            PlayerHealthState = PlayerHealthState.Alive;
            PowerUp = PowerUpType.None;

            if (Lives < 0)
            {
                GameOver(this, EventArgs.Empty);
                Lives = 3;
            }

            base.Reset();
        }

        public void GotEaten()
        {
            PlayerHealthState = PlayerHealthState.Dead;
            SoundEffectManager.PlayPlayerSound();
            Lives--;
        }

        public void ActivatePowerUp(ItemType aItemType)
        {
            const int ghostEaterDuration = 5000;
            const int wallUnlockerDuration = 3000;

            switch (aItemType)
            {
                case ItemType.Pellet:
                    break;
                case ItemType.Cherry:
                    PowerUp = PowerUpType.GhostEater;
                    myDurationTimer = ghostEaterDuration;
                    break;
                case ItemType.Key:
                    PowerUp = PowerUpType.WallUnlocker;
                    myDurationTimer = wallUnlockerDuration;
                    break;
            }
        }

        public override void Update(GameTime aGameTime)
        {
            UpdatePowerUps(aGameTime);
            base.Update(aGameTime);
        }
        #endregion

        #region Protected methods
        override protected Vector2? NextTarget()
        {
            if (PowerUp != PowerUpType.WallUnlocker)
            {
                return base.NextTarget();
            }
            else
            {
                return NextWallUnlockerTarget();
            }
        }

        override protected void Movement(GameTime aGameTime)
        {
            if (PlayerHealthState == PlayerHealthState.Alive)
            {
                if (PlayerInput.GetDirection() != Direction.NONE)
                {
                    Direction = PlayerInput.GetDirection();
                }

                RotateAnimation();
                base.Movement(aGameTime);
            }
        }

        protected override void CalculateSourceRectangle(GameTime aGameTime)
        {
            const int NumberOfDeathFrames = 2;
            myFrameTimeCounterMilliseconds -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myFrameTimeCounterMilliseconds <= 0)
            {
                myFrameTimeCounterMilliseconds = myTimePerFrameMilliseconds;

                if (PlayerHealthState == PlayerHealthState.Alive)
                {
                    UpdateAliveAnimation(NumberOfDeathFrames);
                }
                else
                {
                    UpdateDeathAnimation(NumberOfDeathFrames);
                }
            }
        }
        #endregion

        #region Private method

        private void UpdateAliveAnimation(int aNumberOfDeathFrames)
        {
            if (FrameXIndex >= NumberOfXFrames - (1 + aNumberOfDeathFrames))
            {
                FrameXIndex = 0;
            }
            else
            {
                FrameXIndex++;
            }
        }

        private void UpdateDeathAnimation(int aNumberOfDeathFrames)
        {
            if (FrameXIndex < NumberOfXFrames - aNumberOfDeathFrames)
            {
                FrameXIndex = 3;
            }
            else
            {
                if (FrameXIndex < NumberOfXFrames - 1)
                {
                    FrameXIndex++;
                }
            }
        }

        private void RotateAnimation()
        {
            switch (Direction)
            {
                case Direction.Up:
                    SpriteEffect = SpriteEffects.None;
                    Rotation = MathHelper.ToRadians(90);
                    break;
                case Direction.Left:
                    SpriteEffect = SpriteEffects.None;
                    Rotation = 0;
                    break;
                case Direction.Down:
                    SpriteEffect = SpriteEffects.None;
                    Rotation = MathHelper.ToRadians(-90);
                    break;
                case Direction.Right:
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                    Rotation = 0;
                    break;
            }
        }

        private void UpdatePowerUps(GameTime aGameTime)
        {
            switch (PowerUp)
            {
                case PowerUpType.None:
                    break;
                case PowerUpType.GhostEater:
                    UpdatePowerUpTimer(aGameTime);
                    break;
                case PowerUpType.WallUnlocker:
                    UpdatePowerUpTimer(aGameTime);
                    break;
            }
        }

        private void UpdatePowerUpTimer(GameTime aGameTime)
        {
            myDurationTimer -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myDurationTimer <= 0)
            {
                PowerUp = PowerUpType.None;
            }
        }

        private void InitializeMemberVariables()
        {
            PlayerHealthState = PlayerHealthState.Alive;
            Direction = Direction.Left;
            myDurationTimer = 0;
            PowerUp = PowerUpType.None;
            Lives = 3;
        }

        private Vector2? NextWallUnlockerTarget()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return myGameBoard.ValidWallUnlockerTargetPosition(Row - 1, Column);
                case Direction.Left:
                    return myGameBoard.ValidWallUnlockerTargetPosition(Row, Column - 1);
                case Direction.Down:
                    return myGameBoard.ValidWallUnlockerTargetPosition(Row + 1, Column);
                case Direction.Right:
                    return myGameBoard.ValidWallUnlockerTargetPosition(Row, Column + 1);
            }
            return null;
        }
        #endregion
    }
}
