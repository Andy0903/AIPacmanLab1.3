using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Utilities
{
    static class XboxControllerUtility
    {
        #region Member variables
        static GamePadState myNewGamePadStateOne;
        static GamePadState myOldGamePadStateOne;

        static GamePadState myNewGamePadStateTwo;
        static GamePadState myOldGamePadStateTwo;

        static GamePadState myNewGamePadStateThree;
        static GamePadState myOldGamePadStateThree;

        static GamePadState myNewGamePadStateFour;
        static GamePadState myOldGamePadStateFour;
        #endregion

        #region Properties
        static public double ThumbStickSensitivity
        {
            get { return 0.2f; }
        }

        static public double GetLeftThumbStickX(PlayerIndex aPlayerIndex)
        {
            switch (aPlayerIndex)
            {
                case PlayerIndex.One:
                    return myNewGamePadStateOne.ThumbSticks.Left.X;
                case PlayerIndex.Two:
                    return myNewGamePadStateTwo.ThumbSticks.Left.X;
                case PlayerIndex.Three:
                    return myNewGamePadStateThree.ThumbSticks.Left.X;
                case PlayerIndex.Four:
                    return myNewGamePadStateFour.ThumbSticks.Left.X;
            }
            return 0;
        }

        static public double GetLeftThumbStickY(PlayerIndex aPlayerIndex)
        {
            switch (aPlayerIndex)
            {
                case PlayerIndex.One:
                    return myNewGamePadStateOne.ThumbSticks.Left.Y;
                case PlayerIndex.Two:
                    return myNewGamePadStateTwo.ThumbSticks.Left.Y;
                case PlayerIndex.Three:
                    return myNewGamePadStateThree.ThumbSticks.Left.Y;
                case PlayerIndex.Four:
                    return myNewGamePadStateFour.ThumbSticks.Left.Y;
            }
            return 0;
        }
        #endregion

        #region Public methods
        static public bool WasClicked(PlayerIndex aPlayerIndex, Buttons aButton)
        {
            switch (aPlayerIndex)
            {
                case PlayerIndex.One:
                    return myOldGamePadStateOne.IsButtonUp(aButton) && myNewGamePadStateOne.IsButtonDown(aButton);
                case PlayerIndex.Two:
                    return myOldGamePadStateTwo.IsButtonUp(aButton) && myNewGamePadStateTwo.IsButtonDown(aButton);
                case PlayerIndex.Three:
                    return myOldGamePadStateThree.IsButtonUp(aButton) && myNewGamePadStateThree.IsButtonDown(aButton);
                case PlayerIndex.Four:
                    return myOldGamePadStateFour.IsButtonUp(aButton) && myNewGamePadStateFour.IsButtonDown(aButton);
            }
            return false;
        }

        static public bool ControllerConnected(PlayerIndex? aPlayerIndex = null)
        {
            switch (aPlayerIndex)
            {
                case PlayerIndex.One:
                    return myNewGamePadStateOne.IsConnected;
                case PlayerIndex.Two:
                    return myNewGamePadStateTwo.IsConnected;
                case PlayerIndex.Three:
                    return myNewGamePadStateTwo.IsConnected;
                case PlayerIndex.Four:
                    return myNewGamePadStateTwo.IsConnected;
                case null:
                    if (myNewGamePadStateOne.IsConnected ||
                        myNewGamePadStateTwo.IsConnected ||
                        myNewGamePadStateThree.IsConnected ||
                        myNewGamePadStateFour.IsConnected)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }

        static public void Update()
        {
            myOldGamePadStateOne = myNewGamePadStateOne;
            myNewGamePadStateOne = GamePad.GetState(PlayerIndex.One);

            myOldGamePadStateTwo = myNewGamePadStateTwo;
            myNewGamePadStateTwo = GamePad.GetState(PlayerIndex.Two);

            myOldGamePadStateThree = myNewGamePadStateThree;
            myNewGamePadStateThree = GamePad.GetState(PlayerIndex.Three);

            myOldGamePadStateFour = myNewGamePadStateFour;
            myNewGamePadStateFour = GamePad.GetState(PlayerIndex.Four);
        }

        #endregion
    }
}
