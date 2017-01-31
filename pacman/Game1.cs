using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Utilities;

namespace Pacman
{
    public class Game1 : Game
    {
        #region Member variables
        GraphicsDeviceManager myGraphics;
        SpriteBatch mySpriteBatch;

        SpriteFont myFont;
        GameBoard myGameBoard;
        MenuManager myMenuManager;
        SongManager mySoundManager;

        public static ContentManager myContentManager;

        GameState myGameState;
        GameState myOldGameState;
        #endregion

        #region Constructors
        public Game1()
        {
            myGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #endregion

        #region Protected methods
        protected override void Initialize()
        {
            WindowManager.ApplyCustomWindowChanges(Window, myGraphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            InitializeLoadContentMemberVariables();

            myFont = myContentManager.Load<SpriteFont>("Font");

            InitializeMemberVariables();
            SetMenuManagerListeners();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime aGameTime)
        {
            UpdateUtilities();
            switch (myGameState)
            {
                case GameState.Playing:
                    //Camera.Update();
                    myGameBoard.Update(aGameTime);
                    SoundEffectManager.Update(aGameTime);
                    break;
                case GameState.Menu:
                    // Camera.Reset();
                    myMenuManager.Update();
                    break;
            }
            UpdateSoundManager();

            base.Update(aGameTime);
        }

        protected override void Draw(GameTime aGameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // mySpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, Camera.TranslationMatrix);
            mySpriteBatch.Begin();

            switch (myGameState)
            {
                case GameState.Playing:
                    myGameBoard.Draw(mySpriteBatch);
                    break;
                case GameState.Menu:
                    myMenuManager.Draw(mySpriteBatch);
                    break;
            }

            mySpriteBatch.End();
            base.Draw(aGameTime);
        }
        #endregion

        #region Private methods
        private void UpdateUtilities()
        {
            KeyboardUtility.Update();
            XboxControllerUtility.Update();
        }

        private void SetMenuManagerListeners()
        {
            myMenuManager.ExitSelected += MenuExitGame;
            myMenuManager.StartSelected += MenuResetAndStartGame;
        }

        private void SetGameBoardListeners()
        {
            myGameBoard.GameOver += MenuGameOver;
            myGameBoard.Player.GameOver += MenuGameOver;
            myGameBoard.WonGame += MenuWonGame;
        }

        private void MenuExitGame(object aSender, EventArgs aEventArg)
        {
            Exit();
        }

        private void MenuWonGame(object aSender, EventArgs aEventArg)
        {
            myGameState = GameState.Menu;
            myMenuManager.MenuState = MenuState.Winning;

            Highscore.UpdateHighscore(GameBoard.Score);
        }

        private void MenuGameOver(object aSender, EventArgs aEventArg)
        {
            myGameState = GameState.Menu;
            myMenuManager.MenuState = MenuState.GameOver;

            Highscore.UpdateHighscore(GameBoard.Score);
        }


        private void MenuResetAndStartGame(object aSender, EventArgs aEventArgs)
        {
            myGameBoard = new GameBoard(myFont);
            SetGameBoardListeners();
            myGameBoard.Reset();
            myGameState = GameState.Playing;
        }

        private void InitializeMemberVariables()
        {
            myGameState = GameState.Menu;
            mySoundManager = new SongManager();
            myMenuManager = new MenuManager(myFont);
            SoundEffectManager.InitalizeVariables();
            PlayerInput.Initialize();
            //Camera.Reset();
        }

        private void InitializeLoadContentMemberVariables()
        {
            myContentManager = Content;
            mySpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void UpdateSoundManager()
        {
            if (myOldGameState != myGameState)
            {
                mySoundManager.Update(myGameState);
            }

            myOldGameState = myGameState;
        }
        #endregion

    }
}
