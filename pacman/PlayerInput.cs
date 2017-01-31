using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    static class PlayerInput
    {
        static Hashtable<Keys, Direction> myKeybindings;
        static bool myRemovedAKey;
        static Keys myRecentlyRemovedKey;
        static Direction myRecentlyRemovedDirection;

        public static void Initialize()
        {
            myKeybindings = new Hashtable<Keys, Direction>(20);
            myRemovedAKey = false;
            SetDefaultKeybindings();
        }

        private static void SetDefaultKeybindings()
        {
            myKeybindings.Put(Keys.W, Direction.Up);
            myKeybindings.Put(Keys.Up, Direction.Up);

            myKeybindings.Put(Keys.A, Direction.Left);
            myKeybindings.Put(Keys.Left, Direction.Left);

            myKeybindings.Put(Keys.S, Direction.Down);
            myKeybindings.Put(Keys.Down, Direction.Down);

            myKeybindings.Put(Keys.D, Direction.Right);
            myKeybindings.Put(Keys.Right, Direction.Right);
        }

        public static void CheckIfClickedAssignedKey()
        {
            if (myRemovedAKey == false)
            {
                List<KeyValuePair<Keys, Direction>> list = myKeybindings.ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    if (Utilities.KeyboardUtility.GetClickedKeys().Contains(list[i].Key))
                    {
                        myKeybindings.Remove(list[i].Key);
                        myRemovedAKey = true;
                        myRecentlyRemovedKey = list[i].Key;
                        myRecentlyRemovedDirection = list[i].Value;
                    }
                }
            }
        }

        public static void RebindToNewKey()
        {
            if (myRemovedAKey == true)
            {
                if (Utilities.KeyboardUtility.GetClickedKeys().Count() > 0)
                {
                    if (Utilities.KeyboardUtility.GetLastClickedKey() != myRecentlyRemovedKey &&
                        myKeybindings.ContainsKey(Utilities.KeyboardUtility.GetLastClickedKey()) == false)
                    {
                        myKeybindings.Put(Utilities.KeyboardUtility.GetLastClickedKey(), myRecentlyRemovedDirection);
                        myRemovedAKey = false;
                        myRecentlyRemovedDirection = Direction.NONE;
                    }
                }
            }
        }

        public static string GetKeyText(Direction aDirection, int aSlot)
        {
            List<KeyValuePair<Keys, Direction>> list = myKeybindings.ToList();
            int counter = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Value == aDirection)
                {
                    if (counter < aSlot)
                    {
                        ++counter;
                        continue;
                    }
                    return list[i].Key.ToString();
                }
            }
            return "";
        }

        public static Direction GetDirection()
        {
            Direction direction = Direction.NONE;
            foreach (Keys key in Utilities.KeyboardUtility.GetClickedKeys())
            {
                myKeybindings.TryGetValue(key, out direction);
            }

            return direction;
        }
    }
}
