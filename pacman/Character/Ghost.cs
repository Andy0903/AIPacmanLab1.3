using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    abstract class Ghost : Character
    {
        #region Member variables
        //GhostHealthState myGhostHealthState;
        IGhostState myCurrentBehaviour;
        public readonly int DEFAULT_FRAME_Y_INDEX;
        #endregion

        #region Properties
        public ConsoleColor Color
        {
            get;
            private set;
        }

        public Player Player
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public Ghost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition, int aDefaultFrameYIndex, ConsoleColor aColor)
            : base("Ghosts", aPosition, 32, 8, 5, 2, 1, 100, aGameBoard, 280)
        {
            DEFAULT_FRAME_Y_INDEX = aDefaultFrameYIndex;
            InitializeMemberVariables(aPlayer, aColor);
        }
        #endregion

        #region Public methods
        public override void Reset()
        {
            SetDefaultNonParameterMemberVariables();
            base.Reset();
        }

        public override void Update(GameTime aGameTime)
        {
            //UpdateHealthState();
            //Collision();
            myCurrentBehaviour.Execute(this);
            base.Update(aGameTime);
        }
        #endregion

        #region Protected methods    
        protected override Vector2? NextTarget()
        {
            //switch (myGhostHealthState)
            //{
            //    case GhostHealthState.Alive:
            //        return GoToPosition(Player.Column, Player.Row);
            //    case GhostHealthState.Scared:
            //        return AvoidPosition(Player.Position);
            //    case GhostHealthState.Dead:
            //        return GoToPosition((int)SpawnPosition.X / Tile.Size, (int)SpawnPosition.Y / Tile.Size);
            //}
            //return null;

            return myCurrentBehaviour.FindPath(this);
        }

        public Vector2? AvoidPosition(Vector2 aPosition)
        {
            Direction bestDirection = Direction;
            Vector2? bestTarget = null;
            float bestDistance = Vector2.Distance(Position, aPosition);

            //Foreach direction in enum Direction
            foreach (Direction direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                Vector2? target = NextTarget(direction);

                if (target != null)
                {
                    float distance = Vector2.Distance((Vector2)target, aPosition);
                    if (bestDistance < distance)
                    {
                        bestDirection = direction;
                        bestTarget = target;
                        bestDistance = distance;
                    }
                }
            }

            Direction = bestDirection;
            return bestTarget;
        }

        protected Vector2? NextTarget(Direction aDirection)
        {
            switch (aDirection)
            {
                case Direction.Up:
                    return myGameBoard.ValidTargetPosition(Row - 1, Column);
                case Direction.Left:
                    return myGameBoard.ValidTargetPosition(Row, Column - 1);
                case Direction.Down:
                    return myGameBoard.ValidTargetPosition(Row + 1, Column);
                case Direction.Right:
                    return myGameBoard.ValidTargetPosition(Row, Column + 1);
            }
            return null;
        }

        public Vector2? GoToPosition(int aColumn, int aRow)
        {
            Tile nextTile = FindNextTile(aColumn, aRow);

            if (nextTile == null)
            {
                return null;
            }

            Vector2? bestTarget = myGameBoard.ValidTargetPosition(nextTile.Row, nextTile.Column);
            SetDirectionTowardTile(nextTile);

            return bestTarget;
        }

        override protected void CalculateSourceRectangle(GameTime aGameTime)
        {
            myFrameTimeCounterMilliseconds -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myFrameTimeCounterMilliseconds <= 0)
            {
                myFrameTimeCounterMilliseconds = myTimePerFrameMilliseconds;

                //switch (myGhostHealthState)
                //{
                //    case GhostHealthState.Alive:
                //        ChangeSourceRectangleAlive();
                //        break;
                //    case GhostHealthState.Scared:
                //        ChangeSourceRectangleScared();
                //        break;
                //    case GhostHealthState.Dead:
                //        ChangeSourceRectangleDead();
                //        break;
                //}
                myCurrentBehaviour.ExecuteGraphics(this);
            }
        }
        #endregion

        #region Private methdos
        private void InitializeMemberVariables(Player aPlayer, ConsoleColor aColor)
        {
            InitializeParameterMemberVariables(aPlayer, aColor);
            //SetDefaultNonParameterMemberVariables(); NOT NEEDED DUE TO RESET BEING CALLED.
        }

        private void InitializeParameterMemberVariables(Player aPlayer, ConsoleColor aColor)
        {
            Player = aPlayer;
            Color = aColor;
        }

        private void SetDefaultNonParameterMemberVariables()
        {
            //myGhostHealthState = GhostHealthState.Alive;
            ChangeState(new SAlive());
        }

        public void ChangeState(IGhostState aState)
        {
            if (myCurrentBehaviour != null)
            {
                myCurrentBehaviour.Exit(this);
            }
            myCurrentBehaviour = aState;
            aState.Enter(this);
        }

        //private void Collision()
        //{
        //    //if (myGhostHealthState != GhostHealthState.Dead)
        //    //{
        //    //    if (CollisionWithPlayer())
        //    //    {
        //    //        switch (Player.PowerUp)
        //    //        {
        //    //            case PowerUpType.None:
        //    //            case PowerUpType.WallUnlocker:
        //    //                Player.GotEaten();
        //    //                break;
        //    //            case PowerUpType.GhostEater:
        //    //                GotEaten();
        //    //                break;
        //    //        }
        //    //    }
        //    //}
        //}

        public bool CollisionWithPlayer()
        {
            if (Player.SizeHitbox.Intersects(SizeHitbox))
            {
                return true;
            }
            return false;
        }

        //private void UpdateHealthState()
        //{
        //    //if (myGhostHealthState != GhostHealthState.Dead)
        //    //{
        //    //    switch (Player.PowerUp)
        //    //    {
        //    //        case PowerUpType.None:
        //    //        case PowerUpType.WallUnlocker:
        //    //            myGhostHealthState = GhostHealthState.Alive;
        //    //            break;
        //    //        case PowerUpType.GhostEater:
        //    //            myGhostHealthState = GhostHealthState.Scared;
        //    //            break;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (Position == SpawnPosition)
        //    //    {
        //    //        myGhostHealthState = GhostHealthState.Alive;
        //    //    }
        //    //}
        //}

        //public void GotEaten()
        //{
        //    // myGhostHealthState = GhostHealthState.Dead;
        //    ChangeState(new SDead());
        //    // SoundEffectManager.PlayGhostSound();
        //}

        //private void ChangeSourceRectangleDead()
        //{
        //    myFrameYIndex = 4;
        //    switch (Direction)
        //    {
        //        case Direction.Up:
        //            if (myFrameXIndex != 6)
        //            {
        //                myFrameXIndex = 6;
        //            }
        //            break;
        //        case Direction.Left:
        //            if (myFrameXIndex != 5)
        //            {
        //                myFrameXIndex = 5;
        //            }
        //            break;
        //        case Direction.Down:
        //            if (myFrameXIndex != 7)
        //            {
        //                myFrameXIndex = 7;
        //            }
        //            break;
        //        case Direction.Right:
        //            if (myFrameXIndex != 4)
        //            {
        //                myFrameXIndex = 4;
        //            }
        //            break;
        //    }
        //}

        //private void ChangeSourceRectangleScared()
        //{
        //    myFrameYIndex = 4;

        //    switch (myFrameXIndex)
        //    {
        //        case 0:
        //        case 1:
        //        case 2:
        //            myFrameXIndex++;
        //            break;
        //        case 3:
        //        default:
        //            myFrameXIndex = 0;
        //            break;
        //    }
        //}

        //private void ChangeSourceRectangleAlive()
        //{
        //    myFrameYIndex = myDefaultFrameYIndex;
        //    switch (Direction)
        //    {
        //        case Direction.Up:
        //            myFrameXIndex = myFrameXIndex != 4 ? 4 : 5;
        //            break;
        //        case Direction.Left:
        //            myFrameXIndex = myFrameXIndex != 2 ? 2 : 3;
        //            break;
        //        case Direction.Down:
        //            myFrameXIndex = myFrameXIndex != 6 ? 6 : 7;
        //            break;
        //        case Direction.Right:
        //            myFrameXIndex = myFrameXIndex != 0 ? 0 : 1;
        //            break;
        //    }
        //}

        private void SetDirectionTowardTile(Tile aTile)
        {
            if (aTile.Column == Column && aTile.Row == Row - 1)
            {
                Direction = Direction.Up;
            }
            else if (aTile.Column == Column - 1 && aTile.Row == Row)
            {
                Direction = Direction.Left;
            }
            else if (aTile.Column == Column + 1 && aTile.Row == Row)
            {
                Direction = Direction.Right;
            }
            else if (aTile.Column == Column && aTile.Row == Row + 1)
            {
                Direction = Direction.Down;
            }

        }

        protected abstract List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal);

        protected List<Tile> GetPathList(Tile aCurrentlyWorkingOn, Tile aStart, Hashtable<Tile, Tile> aVisited)
        {
            List<Tile> path = new List<Tile>();

            while (aCurrentlyWorkingOn != null)
            {
                Tile cameFrom;
                if (aVisited.TryGetValue(aCurrentlyWorkingOn, out cameFrom))
                {
                    path.Add(aCurrentlyWorkingOn);
                    if (cameFrom == aStart)
                    {
                        path.Reverse();
                        return path;
                    }
                }
                else
                {
                    return null;
                }

                aCurrentlyWorkingOn = cameFrom;
            }
            return null;
        }

        private Tile FindNextTile(int aColumn, int aRow)
        {
            Tile start = myGameBoard.GetWalkableTile(Row, Column);
            Tile goal = myGameBoard.GetWalkableTile(aRow, aColumn);
            Graph graph = new Graph(myGameBoard);

            List<Tile> path = FindPath(graph, start, goal);

            return (path == null || path.Count < 1) ? null : path[0];
        }
        #endregion
    }
}
