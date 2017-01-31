using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    static class KeyboardUtility
    {
        #region Member variables
        static KeyboardState myOldKeyboardState;
        static KeyboardState myNewKeyboardState;
        #endregion

        #region Public methods
        static public Keys GetLastClickedKey()
        {
            Keys[] recentlyClickedKeys = GetClickedKeys();

            return recentlyClickedKeys.Last<Keys>();
        }

        static public Keys[] GetClickedKeys()
        {
            Keys[] oldKeys = myOldKeyboardState.GetPressedKeys();
            Keys[] newKeys = myNewKeyboardState.GetPressedKeys();

            IEnumerable<Keys> recentlyClickedKeys = newKeys.Except(oldKeys);
            return recentlyClickedKeys.ToArray<Keys>();
        }

        static public bool WasClicked(Keys aKey)
        {
            return myOldKeyboardState.IsKeyUp(aKey) && myNewKeyboardState.IsKeyDown(aKey);
        }

        static public bool IsHeldDown(Keys aKey)
        {
            return myOldKeyboardState.IsKeyDown(aKey) && myNewKeyboardState.IsKeyDown(aKey);
        }

        static public void Update()
        {
            myOldKeyboardState = myNewKeyboardState;
            myNewKeyboardState = Keyboard.GetState();
        }

        #endregion
    }
}
