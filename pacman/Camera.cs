//using Microsoft.Xna.Framework;
//using Utilities;

//namespace Pacman
//{
//    static class Camera
//    {
//        #region Member variables
//        static Rectangle myLevelSize;
//        static Entity myTarget;
//        static float Zoom = 1f;
//        #endregion

//        #region Properties
//        static public float TranslationX
//        {
//            get;
//            private set;
//        }

//        static public float TranslationY
//        {
//            get;
//            private set;
//        }

//        static public Matrix TranslationMatrix
//        {
//            get;
//            private set;
//        }
//        #endregion

//        #region Public methods
//        static public void Initlaize(Entity aTarget, Rectangle aLevelSize)
//        {
//            InitializeMemberVariables(aTarget, aLevelSize);
//        }

//        static public void Update()
//        {
//            TranslationX = -MathHelper.Clamp(myTarget.Position.X - WindowManager.WindowWidth / (2 * Zoom), 0, myLevelSize.Width - WindowManager.WindowWidth);
//            TranslationY = -MathHelper.Clamp(myTarget.Position.Y - WindowManager.WindowHeight / (2 * Zoom), 0, myLevelSize.Height - WindowManager.WindowHeight);

//            TranslationMatrix = Matrix.CreateTranslation(TranslationX, TranslationY, 0) * Matrix.CreateScale(Zoom, Zoom, 1);
//        }

//        static public void Reset()
//        {
//            TranslationMatrix = Matrix.CreateTranslation(0, 0, 0);
//        }
//        #endregion

//        #region Private methods
//        static private void InitializeMemberVariables(Entity aTarget, Rectangle aLevelSize)
//        {
//            myTarget = aTarget;
//            myLevelSize = aLevelSize;
//        }
//        #endregion
//    }
//}
