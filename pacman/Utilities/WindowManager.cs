using Microsoft.Xna.Framework;

namespace Utilities
{
    static class WindowManager
    {
        public static int WindowWidth
        {
            get { return 1280; }
        }

        public static int WindowHeight
        {
            get { return 960; }
        }

        static public void ApplyCustomWindowChanges(GameWindow aWindow, GraphicsDeviceManager aGraphics)
        {
            aWindow.Position = new Point(0, 0);
            aGraphics.PreferredBackBufferWidth = WindowWidth;
            aGraphics.PreferredBackBufferHeight = WindowHeight;
            aWindow.AllowUserResizing = true;
            aGraphics.ApplyChanges();
        }
    }
}
