using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Utilities;

namespace Pacman
{
    class OptionMenu : Menu
    {
        #region Constructors
        List<Tooltip> myTooltips;

        public OptionMenu(SpriteFont aFont)
                : base(aFont)
        {

            myTooltips = new List<Tooltip>();

            myTooltips.Add(new Tooltip(
                new Vector2(WindowManager.WindowWidth / 2 - 75f / 2,
                WindowManager.WindowHeight - WindowManager.WindowHeight / 1.31f), Direction.Up, 0));

            myTooltips.Add(new Tooltip(
                 new Vector2(WindowManager.WindowWidth / 2 - 75f / 2,
                  WindowManager.WindowHeight - WindowManager.WindowHeight / 1.18f), Direction.Up, 1));

            myTooltips.Add(new Tooltip(
                    new Vector2(WindowManager.WindowWidth - WindowManager.WindowWidth / 1.57f,
                    WindowManager.WindowHeight - WindowManager.WindowHeight / 1.55f), Direction.Left, 0));

            myTooltips.Add(new Tooltip(
                    new Vector2(WindowManager.WindowWidth - WindowManager.WindowWidth / 1.427f,
                    WindowManager.WindowHeight - WindowManager.WindowHeight / 1.55f), Direction.Left, 1));

            myTooltips.Add(new Tooltip(
                    new Vector2(WindowManager.WindowWidth / 2 - 75f / 2,
                WindowManager.WindowHeight - WindowManager.WindowHeight / 1.83f), Direction.Down, 0));

            myTooltips.Add(new Tooltip(
                    new Vector2(WindowManager.WindowWidth / 2 - 75f / 2,
                WindowManager.WindowHeight - WindowManager.WindowHeight / 2.165f), Direction.Down, 1));

            myTooltips.Add(new Tooltip(
                    new Vector2(WindowManager.WindowWidth - WindowManager.WindowWidth / 2.46f,
                    WindowManager.WindowHeight - WindowManager.WindowHeight / 1.55f), Direction.Right, 0));

            myTooltips.Add(new Tooltip(
                    new Vector2(WindowManager.WindowWidth - WindowManager.WindowWidth / 2.93f,
                    WindowManager.WindowHeight - WindowManager.WindowHeight / 1.55f), Direction.Right, 1));
        }

        #endregion

        #region Events
        public event EventHandler MenuSelected;
        #endregion

        #region Public methods
        public override void Update()
        {
            foreach (Tooltip tooltip in myTooltips)
            {
                tooltip.Update();
            }
            PlayerInput.CheckIfClickedAssignedKey();
            PlayerInput.RebindToNewKey();
            base.Update();
        }
        #endregion

        #region Protected methods
        override protected void KeyPressing()
        {
            if (KeyboardUtility.WasClicked(Keys.Escape) == true || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.B))
            {
                MenuSelected(this, EventArgs.Empty);
            }
        }

        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, "Options", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            DrawKeybindings(aSpriteBatch);
        }

        override protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ESCAPE to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 4f, Color.Red);
        }

        override protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press B to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Yellow);
        }
        #endregion

        #region Private methods
        private void DrawKeybindings(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "UP",
                WindowManager.WindowHeight - WindowManager.WindowHeight / 1.5f, Color.Gold);

            OutlinedText.DrawCenteredText(aSpriteBatch, myFont, 1.5f, "LEFT",
                new Vector2(WindowManager.WindowWidth - WindowManager.WindowWidth / 1.8f, WindowManager.WindowHeight - WindowManager.WindowHeight / 1.65f), Color.Turquoise);
            
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "DOWN",
              WindowManager.WindowHeight - WindowManager.WindowHeight / 1.8f, Color.Lime);
            
            OutlinedText.DrawCenteredText(aSpriteBatch, myFont, 1.5f, "RIGHT",
                new Vector2(WindowManager.WindowWidth - WindowManager.WindowWidth / 2.3f, WindowManager.WindowHeight - WindowManager.WindowHeight / 1.65f), Color.DarkOrange);

            for (int i = 0; i < myTooltips.Count; i++)
            {
                switch (i)
                {
                    case 0:
                    case 1:
                        myTooltips[i].Draw(aSpriteBatch, Color.Gold);
                        break;
                    case 2:
                    case 3:
                        myTooltips[i].Draw(aSpriteBatch, Color.Turquoise);
                        break;
                    case 4:
                    case 5:
                        myTooltips[i].Draw(aSpriteBatch, Color.Lime);
                        break;
                    case 6:
                    case 7:
                        myTooltips[i].Draw(aSpriteBatch, Color.DarkOrange);
                        break;
                }
            }
            
        }
        #endregion
    }
}