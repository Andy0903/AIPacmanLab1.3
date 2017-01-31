using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Pacman
{
    public static class SoundEffectManager
    {
        #region Member variables
        static SoundEffect myPlayerDeathSound;
        static SoundEffectInstance myPlayerDeathSoundInstance;
        static int myPlayerDeathSoundDuration;

        static SoundEffect myEatSound;
        static SoundEffectInstance myEatGhostSoundInstance;
        static SoundEffectInstance myEatItemSoundInstance;
        static int myItemEatSoundDuration;
        static int myGhostEatSoundDuration;

        #endregion

        #region Public methods
        public static void Update(GameTime aGameTime)
        {
            UpdatePlayerSound(aGameTime);
            UpdateGhostSound(aGameTime);
            UpdateItemSound(aGameTime);
        }

        public static void PlayGhostSound()
        {
            if (myEatGhostSoundInstance.State == SoundState.Stopped)
            {
                myEatGhostSoundInstance.Play();
                myGhostEatSoundDuration = 500;    //if higher -> static sound after play.
            }
        }

        public static void PlayPlayerSound()
        {
            if (myPlayerDeathSoundInstance.State == SoundState.Stopped)
            {
                myPlayerDeathSoundInstance.Play();
                myPlayerDeathSoundDuration = 1008;    //if higher -> static sound after play.
            }
        }

        public static void PlayItemSound()
        {
            if (myEatItemSoundInstance.State == SoundState.Stopped)
            {
                myEatItemSoundInstance.Play();
                myItemEatSoundDuration = 65;
            }
        }

        public static void InitalizeVariables()
        {
            InitializePlayerVariables();
            InitializeEatSoundVariables();
        }
        #endregion

        #region Private methods
        private static void InitializePlayerVariables()
        {
            myPlayerDeathSound = Game1.myContentManager.Load<SoundEffect>("PlayerDeathSound");
            myPlayerDeathSoundInstance = myPlayerDeathSound.CreateInstance();
        }

        private static void InitializeEatSoundVariables()
        {
            myEatSound = Game1.myContentManager.Load<SoundEffect>("EatSound");
            myEatGhostSoundInstance = myEatSound.CreateInstance();
            myEatItemSoundInstance = myEatSound.CreateInstance();
        }

        private static void UpdateGhostSound(GameTime aGameTime)
        {
            myGhostEatSoundDuration -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myGhostEatSoundDuration <= 0)
            {
                myEatGhostSoundInstance.Stop();
            }
        }

        private static void UpdateItemSound(GameTime aGameTime)
        {
            myItemEatSoundDuration -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myItemEatSoundDuration <= 0)
            {
                myEatItemSoundInstance.Stop();
            }
        }

        private static void UpdatePlayerSound(GameTime aGameTime)
        {
            myPlayerDeathSoundDuration -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myPlayerDeathSoundDuration <= 0)
            {
                myPlayerDeathSoundInstance.Stop();
            }
        }
        #endregion
    }
}
