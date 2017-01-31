using Microsoft.Xna.Framework.Media;

namespace Pacman
{
    class SongManager
    {
        #region Member variables
        Song myMenuSong;
        Song myGameBoardSong;
        #endregion

        #region Constructors
        public SongManager()
        {
            InitializeMemberVariables();
            MediaPlayer.IsRepeating = true;
        }
        #endregion

        #region Public methods
        public void Update(GameState aGameState)
        {
            StopSong();
            PlaySong(aGameState);
        }
        #endregion

        #region Private methods
        private void InitializeMemberVariables()
        {
            myMenuSong = Game1.myContentManager.Load<Song>("MozartSymphony40FirstMovement");
            myGameBoardSong = Game1.myContentManager.Load<Song>("DvorakNewWorld4th");
        }

        private void PlaySong(GameState aGameState)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                switch (aGameState)
                {
                    case GameState.Playing:
                        MediaPlayer.Play(myGameBoardSong);
                        break;
                    case GameState.Menu:
                        MediaPlayer.Play(myMenuSong);
                        break;
                }
            }
        }

        private void StopSong()
        {
            MediaPlayer.Stop();
        }
        #endregion
    }
}
