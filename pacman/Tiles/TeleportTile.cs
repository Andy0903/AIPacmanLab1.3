using Microsoft.Xna.Framework;

namespace Pacman
{
    class TeleportTile : Tile
    {
        #region Member variables
        TeleportTile myTeleportTile;
        float myDurationTimer;
        #endregion

        #region Properties
        public Rectangle SizeHitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Size, Size); }
        }
        #endregion

        #region Constructors
        public TeleportTile(int aGridX, int aGridY)
            : base("DarkBlueTile", aGridX, aGridY)
        {
            InitializeMemberVariables();
            InitializeProperties();
        }
        #endregion

        #region Public methods
        override public void Update(Player aPlayer, GameTime aGameTime)
        {
            SteppedOn(aPlayer);
            UpdateCooldown(aGameTime);
        }

        public void Link(TeleportTile aTeleportTile)
        {
            myTeleportTile = aTeleportTile;
            aTeleportTile.myTeleportTile = this;
        }
        #endregion

        #region Private method
        private void UpdateCooldown(GameTime aGameTime)
        {
            myDurationTimer -= aGameTime.ElapsedGameTime.Milliseconds;
        }

        private void SteppedOn(Player aPlayer)
        {
            if (aPlayer.SizeHitbox.Intersects(SizeHitbox) && myDurationTimer <= 0)
            {
                ActivateTeleport(aPlayer);
            }
        }

        private void InitializeMemberVariables()
        {
            myDurationTimer = 0;
        }

        private void InitializeProperties()
        {
            CanWalkOn = true;
        }

        private void ActivateTeleport(Player aPlayer)
        {
            TeleportPlayer(aPlayer);
            StartTeleportCooldown();
        }

        private void TeleportPlayer(Player aPlayer)
        {
            aPlayer.SetPosition(myTeleportTile.Position);
        }

        private void StartTeleportCooldown()
        {
            const int cooldownDuration = 400;
            myDurationTimer = cooldownDuration;
            myTeleportTile.myDurationTimer = myDurationTimer;
        }
        #endregion
    }
}
