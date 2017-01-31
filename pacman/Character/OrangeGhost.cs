using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class OrangeGhost : Ghost
    {
        #region Constructors
        public OrangeGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 3, ConsoleColor.Yellow)
        {
        }
        #endregion

        #region Protected methods
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return DepthFirstSearch(aGraph, aStart, aGoal);
        }
        #endregion

        #region Private methods
        private List<Tile> DepthFirstSearch(Graph aGraph, Tile aStart, Tile aGoal)
        {
            Hashtable<Tile, Tile> visisted = new Hashtable<Tile, Tile>(400); //value is the tile from which we reached key.
            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(aStart);

            while (stack.Count > 0)
            {
                Tile current = stack.Pop();

                if (current == aGoal)
                {
                    return GetPathList(current, aStart, visisted);
                }

                foreach (Tile neighbour in aGraph.GetNeighbours(current))
                {
                    if (visisted.ContainsKey(neighbour) == false)
                    {
                        stack.Push(neighbour);
                        visisted.Put(neighbour, current);
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
