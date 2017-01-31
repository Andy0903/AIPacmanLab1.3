using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities;

namespace Pacman
{
    class GameBoard
    {
        #region Member variables
        float myResetTimer;
        float myMelonSpawnTimer;

        Tile[][] myTiles;
        List<Ghost> myGhosts;
        GhostFactory myGhostFactory;
        ItemFactory myItemFactory;
        SpriteFont myFont;

        public static Random myRandom = new Random();
        const float MelonSpawnDuration = 10000;
        const float ResetDuration = 1000;
        const int NumberOfLevels = 7;

        #endregion

        #region Properties
        public static int Score
        {
            get;
            set;
        }

        public int CurrentLevel
        {
            get;
            private set;
        }

        public Player Player
        {
            get;
            private set;
        }

        private int OldScore
        {
            get;
            set;
        }

        public int Rows
        {
            get { return myTiles.Length; }
        }
        #endregion

        #region Constructors
        public GameBoard(SpriteFont aFont)
        {
            InitializeMemberVariables(aFont);
        }
        #endregion

        #region Events
        public event EventHandler GameOver;

        public event EventHandler WonGame;
        #endregion

        #region Public methods
        public int Colums(int aRow)
        {
            return myTiles[aRow].Length;
        }

        public void Reset()
        {
            ResetTiles();
            ResetGhosts();
            Player.PrepareNewRound();
            myResetTimer = ResetDuration;
            myMelonSpawnTimer = MelonSpawnDuration;
            ResetScore();
        }

        public void Draw(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            DrawTiles(aSpriteBatch, aColor);
            DrawGhosts(aSpriteBatch, aColor);
            Player.Draw(aSpriteBatch);
            DrawLivesText(aSpriteBatch);
            DrawScoreText(aSpriteBatch);
        }

        public void Update(GameTime aGameTime)
        {
            if (Player.PlayerHealthState == PlayerHealthState.Alive)
            {
                UpdateTiles(aGameTime);
                UpdateWinStatus();
                UpdateGhosts(aGameTime);
            }
            else
            {
                StartResetSequence(aGameTime);
            }
            Player.Update(aGameTime);
        }

        public Vector2? ValidTargetPosition(int aRow, int aColumn)
        {
            if (ValidWalkableTile(aRow, aColumn))
            {
                return new Vector2(aColumn * Tile.Size, aRow * Tile.Size);
            }
            return null;
        }

        public Vector2? ValidWallUnlockerTargetPosition(int aRow, int aColumn)
        {
            if (ValidWalkableTile(aRow, aColumn) ||
                WithinBoardTiles(aRow, aColumn) &&
                ((myTiles[aRow][aColumn] is WallTile) && Player.PowerUp == PowerUpType.WallUnlocker))
            {
                BreakWallTile(aRow, aColumn);
                return new Vector2(aColumn * Tile.Size, aRow * Tile.Size);
            }
            return null;
        }

        public Tile GetWalkableTile(int aRow, int aColumn)
        {
            if (ValidWalkableTile(aRow, aColumn))
            {
                return myTiles[aRow][aColumn];
            }
            return null;
        }

        #endregion

        #region Private methods

        private bool WithinBoardTiles(int aRow, int aColumn)
        {
            if (0 <= aRow && 0 <= aColumn && aRow < myTiles.Length && aColumn < myTiles[aRow].Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidWalkableTile(int aRow, int aColumn)
        {
            if (WithinBoardTiles(aRow, aColumn) && myTiles[aRow][aColumn].CanWalkOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitializeNonParameterMemberVariables()
        {
            myMelonSpawnTimer = MelonSpawnDuration;
            myResetTimer = ResetDuration;
            myGhostFactory = new GhostFactory();
            myItemFactory = new ItemFactory();
            myGhosts = new List<Ghost>();
            InitializeBoardFromFile();
            //Camera.Initlaize(Player, new Rectangle(0, 0, 4000, 4000));
        }

        private void InitializeParameterMemberVariables(SpriteFont aFont)
        {
            myFont = aFont;
        }

        private void InitializeProperties()
        {
            CurrentLevel = 1;
            Player = new Player(this);

            Score = 0;
            OldScore = Score;
        }

        private void InitializeMemberVariables(SpriteFont aFont)
        {
            InitializeProperties();
            InitializeNonParameterMemberVariables();
            InitializeParameterMemberVariables(aFont);
        }

        private void ResetScore()
        {
            if (Player.Lives != 3)
            {
                Score = OldScore;
            }
        }

        private void StartResetSequence(GameTime aGameTime)
        {
            myResetTimer -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myResetTimer <= 0)
            {
                Reset();
            }
        }

        private void ClearBoard()
        {
            myGhosts = new List<Ghost>();
            myTiles = null;
        }

        private void InitializeBoardFromFile()
        {
            List<string> strings = new List<string>();
            
            ReadFromFile(strings);
            InitializeBoard(strings);
        }

        private void ReadFromFile(List<string> aStrings)
        {
            using (StreamReader myStreamReader = new StreamReader("../../../Content/Levels/" + CurrentLevel + ".lvl"))
            {
                while (!myStreamReader.EndOfStream)
                {
                    aStrings.Add(myStreamReader.ReadLine());
                }
            }
        }

        private void BreakWallTile(int aRow, int aColumn)
        {
            if (myTiles[aRow][aColumn] is WallTile && myTiles[aRow][aColumn].CanWalkOn == false)
            {
                (myTiles[aRow][aColumn] as WallTile).Broken = true;
            }

        }

        private bool AllPelletsEaten()
        {
            foreach (Tile[] tiles in myTiles)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile is BlankTile)
                    {
                        BlankTile current = tile as BlankTile;
                        if (current.HasPellet == true)
                        {
                           return false;
                        }
                    }
                }
            }
            return true;
        }

        private void UpdateWinStatus()
        {
            if (AllPelletsEaten())
            {
                ChangeLevel();
            }
        }

        private void ChangeLevel()
        {
            if (CurrentLevel < NumberOfLevels)
            {
                CurrentLevel++;
                OldScore = Score;
                ClearBoard();
                InitializeBoardFromFile();
                Reset();
            }
            else
            {
                WonGame(this, EventArgs.Empty);
            }
        }

        private void GiveMelonToEmptyTiles(GameTime aGameTime)
        {
            myMelonSpawnTimer -= aGameTime.ElapsedGameTime.Milliseconds;
            if (myMelonSpawnTimer <= 0)
            {
                foreach (Tile[] tiles in myTiles)
                {
                    foreach (Tile tile in tiles)
                    {
                        if (tile is BlankTile && !(tile as BlankTile).HasItem)
                        {
                            TryToSpawnMelon(tile as BlankTile);
                        }
                    }
                }
                myMelonSpawnTimer = MelonSpawnDuration;
            }
        }

        private void TryToSpawnMelon(BlankTile aTile)
        {
            int randomNumber = myRandom.Next(100);

            if (randomNumber == 1)
            {
                aTile.AddTemporaryItem(myItemFactory.GetItem(ItemType.Melon, aTile.Position));
            }
        }

        private void RemoveAdjacentBrokenNeighbors(int aRow, int aColumn, WallTile aWallTile)
        {
            if (WallAt(aRow - 1, aColumn) && ((myTiles[aRow - 1][aColumn] as WallTile).Broken == true))
            {
                aWallTile.RemoveNeighbor(WallTileNeighbors.North);
            }

            if (WallAt(aRow, aColumn - 1) && ((myTiles[aRow][aColumn - 1] as WallTile).Broken == true))
            {
                aWallTile.RemoveNeighbor(WallTileNeighbors.West);
            }

            if (WallAt(aRow + 1, aColumn) && ((myTiles[aRow + 1][aColumn] as WallTile).Broken == true))
            {
                aWallTile.RemoveNeighbor(WallTileNeighbors.South);
            }

            if (WallAt(aRow, aColumn + 1) && ((myTiles[aRow][aColumn + 1] as WallTile).Broken == true))
            {
                aWallTile.RemoveNeighbor(WallTileNeighbors.East);
            }
        }

        private void RemoveBrokenNeighborsFromWallTiles()
        {
            for (int i = 0; i < myTiles.Length; i++)
            {
                for (int j = 0; j < myTiles[i].Length; j++)
                {
                    WallTile current = myTiles[i][j] as WallTile;

                    if (current != null)
                    {
                        RemoveAdjacentBrokenNeighbors(i, j, current);
                    }
                }
            }
        }

        private void UpdateTiles(GameTime aGameTime)
        {
            GiveMelonToEmptyTiles(aGameTime);
            RemoveBrokenNeighborsFromWallTiles();

            foreach (Tile[] tiles in myTiles)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile != null)
                    {
                        tile.Update(Player, aGameTime);
                    }
                }
            }
        }

        private void DrawTiles(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            foreach (Tile[] tiles in myTiles)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile != null)
                    {
                        tile.Draw(aSpriteBatch, aColor);
                    }
                }
            }
        }
        
        private void InitializeBoard(List<string> aStrings)
        {
            myTiles = new Tile[aStrings.Count][];
            TeleportTile[] teleportTiles = new TeleportTile[10];

            for (int i = 0; i < aStrings.Count; i++)
            {
                myTiles[i] = new Tile[aStrings[i].Length];

                for (int j = 0; j < aStrings[i].Length; j++)
                {
                    BlankTile current;
                    switch (aStrings[i][j])
                    {
                        case 'w':
                            myTiles[i][j] = new WallTile(j, i);
                            break;
                        case ' ':
                            myTiles[i][j] = new BlankTile(j, i);
                            break;
                        case 'm':
                            myTiles[i][j] = new MapBorderTile(j, i);
                            break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            int serialnumber = int.Parse(aStrings[i][j].ToString());
                            myTiles[i][j] = new TeleportTile(j, i);

                            if (teleportTiles[serialnumber] != null)
                            {
                                teleportTiles[serialnumber].Link(myTiles[i][j] as TeleportTile);
                            }
                            else
                            {
                                teleportTiles[serialnumber] = myTiles[i][j] as TeleportTile;
                            }

                            break;
                        case 'f':
                            myTiles[i][j] = new BlankTile(j, i);
                            current = myTiles[i][j] as BlankTile;
                            current.AddItem(myItemFactory.GetItem(ItemType.Pellet, myTiles[i][j].Position));
                            break;
                        case 'c':
                            myTiles[i][j] = new BlankTile(j, i);
                            current = myTiles[i][j] as BlankTile;
                            current.AddItem(myItemFactory.GetItem(ItemType.Cherry, myTiles[i][j].Position));
                            break;
                        case 'k':
                            myTiles[i][j] = new BlankTile(j, i);
                            current = myTiles[i][j] as BlankTile;
                            current.AddItem(myItemFactory.GetItem(ItemType.Key, myTiles[i][j].Position));
                            break;
                        case 'p':
                            myTiles[i][j] = new BlankTile(j, i);
                            Player.SetSpawnPosition(myTiles[i][j].Position);
                            break;
                        case 'r':
                            myTiles[i][j] = new BlankTile(j, i);
                            myGhosts.Add(myGhostFactory.GetGhost(GhostType.Red, Player, this, myTiles[i][j].Position));
                            myGhosts[myGhosts.Count - 1].SetSpawnPosition(myTiles[i][j].Position);
                            break;
                        case 'v':
                            myTiles[i][j] = new BlankTile(j, i);
                            myGhosts.Add(myGhostFactory.GetGhost(GhostType.Violet, Player, this, myTiles[i][j].Position));
                            myGhosts[myGhosts.Count - 1].SetSpawnPosition(myTiles[i][j].Position);
                            break;
                        case 'b':
                            myTiles[i][j] = new BlankTile(j, i);
                            myGhosts.Add(myGhostFactory.GetGhost(GhostType.Blue, Player, this, myTiles[i][j].Position));
                            myGhosts[myGhosts.Count - 1].SetSpawnPosition(myTiles[i][j].Position);
                            break;
                        case 'o':
                            myTiles[i][j] = new BlankTile(j, i);
                            myGhosts.Add(myGhostFactory.GetGhost(GhostType.Orange, Player, this, myTiles[i][j].Position));
                            myGhosts[myGhosts.Count - 1].SetSpawnPosition(myTiles[i][j].Position);
                            break;
                    }
                }
            }
        }

        private bool WallAt(int aRow, int aColumn)
        {
            return (0 <= aRow && 0 <= aColumn && aRow < myTiles.Length && aColumn < myTiles[aRow].Length && myTiles[aRow][aColumn] is WallTile);
        }

        private void UpdateGhosts(GameTime aGameTime)
        {
            foreach (Ghost ghost in myGhosts)
            {
                ghost.Update(aGameTime);
            }
        }

        private void DrawGhosts(SpriteBatch aSpriteBatch, Color? aColor = null)
        {
            foreach (Ghost ghost in myGhosts)
            {
                ghost.Draw(aSpriteBatch, aColor);
            }
        }

        private void DrawLivesText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Lives: " + Player.Lives, WindowManager.WindowHeight - WindowManager.WindowHeight / 12, Color.Yellow);
        }

        private void DrawScoreText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Score: " + Score, WindowManager.WindowHeight - WindowManager.WindowHeight / 16, Color.Yellow);
        }

        private void ResetGhosts()
        {
            foreach (Ghost ghost in myGhosts)
            {
                ghost.Reset();
            }
        }

        private void ResetTiles()
        {
            foreach (Tile[] tiles in myTiles)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile != null)
                    {
                        tile.Reset();
                    }
                }
            }
            AddNeighborsToWallTiles();
        }

        private void AddNeighborsToWallTiles()
        {
            for (int i = 0; i < myTiles.Length; i++)
            {
                for (int j = 0; j < myTiles[i].Length; j++)
                {
                    WallTile current = myTiles[i][j] as WallTile;

                    if (current != null)
                    {
                        if (WallAt(i - 1, j) && ((myTiles[i - 1][j] as WallTile).Broken == false))
                        {
                            current.AddNeighbor(WallTileNeighbors.North);
                        }

                        if (WallAt(i, j - 1) && ((myTiles[i][j - 1] as WallTile).Broken == false))
                        {
                            current.AddNeighbor(WallTileNeighbors.West);
                        }

                        if (WallAt(i + 1, j) && ((myTiles[i + 1][j] as WallTile).Broken == false))
                        {
                            current.AddNeighbor(WallTileNeighbors.South);
                        }

                        if (WallAt(i, j + 1) && ((myTiles[i][j + 1] as WallTile).Broken == false))
                        {
                            current.AddNeighbor(WallTileNeighbors.East);
                        }
                    }
                }
            }
        }
        #endregion
    }
}