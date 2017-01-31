using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class VioletGhost : Ghost
    {
        #region Constructors
        public VioletGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 1, ConsoleColor.DarkMagenta)
        {
        }
        #endregion

        #region Protected methods
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return DijkstrasAlgorithm(aGraph, aStart, aGoal);
        }
        #endregion

        #region Private methods
        private Tile GetUnvisitedWithShortestDistance(HashSet<Tile> aUnvisited, Hashtable<Tile, float> aDistance)
        {       
            Tile closest = null;
            float minDistance = float.PositiveInfinity;

            foreach (Tile tile in aUnvisited)
            {
                if (aDistance[tile] < minDistance)
                {
                    closest = tile;
                    minDistance = aDistance[tile];
                }
            }

            return closest;

        }

        private List<Tile> DijkstrasAlgorithm(Graph aGraph, Tile aStart, Tile aGoal)
        {
            HashSet<Tile> unvisited = new HashSet<Tile>();
            Hashtable<Tile, Tile> visisted = new Hashtable<Tile, Tile>(400); //value is the tile from which we reached key.

            Hashtable<Tile, float> distance = new Hashtable<Tile, float>(400);

            foreach (Tile tile in aGraph.GetAllTiles())
            {
                distance.Put(tile, float.PositiveInfinity);
                unvisited.Add(tile);
            }

            distance[aStart] = 0;

            while (unvisited.Count > 0)
            {
                Tile minimumDistanceTile = GetUnvisitedWithShortestDistance(unvisited, distance);

                if (minimumDistanceTile == aGoal)
                {
                    return GetPathList(minimumDistanceTile, aStart, visisted);
                }
                
                unvisited.Remove(minimumDistanceTile);

                foreach (Tile neighbour in aGraph.GetNeighbours(minimumDistanceTile))
                {
                    float dist = distance[minimumDistanceTile] + aGraph.GetWeight(minimumDistanceTile, neighbour);

                    if (dist < distance[neighbour])
                    {
                        distance[neighbour] = dist;
                        visisted.Put(neighbour, minimumDistanceTile);
                    }
                }
            }

            return null;
        }

        #endregion
    }
}

