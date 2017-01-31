using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class RedGhost : Ghost
    {
        #region Constructors
        public RedGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 0, ConsoleColor.Red)
        {
        }
        #endregion

        #region Protected methods
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return BreadthFirstSearch(aGraph, aStart, aGoal);
        }
        #endregion

        #region Private methods
        private List<Tile> BreadthFirstSearch(Graph aGraph, Tile aStart, Tile aGoal)
        {
            Queue<Tile> queue = new Queue<Tile>();
            Hashtable<Tile, Tile> visisted = new Hashtable<Tile, Tile>(400); //value is the tile from which we reached key.
            queue.Enqueue(aStart);

            while (queue.Count > 0)
            {
                Tile current = queue.Dequeue();

                if (current == aGoal)
                {
                    return GetPathList(current, aStart, visisted);
                }

                foreach (Tile neighbour in aGraph.GetNeighbours(current))
                {
                    if (visisted.ContainsKey(neighbour) == false)
                    {
                        queue.Enqueue(neighbour);
                        visisted.Put(neighbour, current);
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
