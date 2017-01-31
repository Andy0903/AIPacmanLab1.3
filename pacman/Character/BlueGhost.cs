using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class BlueGhost : Ghost
    {
        #region Constructors
        public BlueGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 2, ConsoleColor.Blue)
        {
        }
        #endregion

        #region Protected methods
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return AStar(aGraph, aStart, aGoal);
        }
        #endregion

        #region Private methods
        private List<Tile> AStar(Graph aGraph, Tile aStart, Tile aGoal)
        {
            HashSet<Tile> alreadyEvaluated = new HashSet<Tile>();
            HashSet<Tile> currentlyDiscovered = new HashSet<Tile>();

            Hashtable<Tile, Tile> visisted = new Hashtable<Tile, Tile>(400); //value is the tile from which we reached key.
            Hashtable<Tile, float> distance = new Hashtable<Tile, float>(400);

            currentlyDiscovered.Add(aStart);
            foreach (Tile tile in aGraph.GetAllTiles())
            {
                distance.Put(tile, float.PositiveInfinity);
            }

            Hashtable<Tile, float> estimatedDistanceToGoal = distance;
            distance[aStart] = 0;
            estimatedDistanceToGoal[aStart] = HeuristicCostEstimate(aStart, aGoal);

            while (0 < currentlyDiscovered.Count)
            {
                Tile current = GetDiscoveredWithLowestCostEstimate(currentlyDiscovered, aGoal);

                if (current == aGoal)
                {
                    return GetPathList(current, aStart, visisted);
                }

                currentlyDiscovered.Remove(current);
                alreadyEvaluated.Add(current);

                foreach (Tile neighbour in aGraph.GetNeighbours(current))
                {
                    if (alreadyEvaluated.Contains(neighbour)) { continue; }

                    float unconfirmedDistance = distance[current] + aGraph.GetWeight(current, neighbour);

                    if (currentlyDiscovered.Contains(neighbour) == false)
                    {
                        currentlyDiscovered.Add(neighbour);
                    }
                    else if(unconfirmedDistance >= distance[neighbour]) { continue; }

                    visisted.Put(neighbour, current);
                    distance[neighbour] = unconfirmedDistance;
                    estimatedDistanceToGoal[neighbour] = distance[neighbour] + HeuristicCostEstimate(neighbour, aGoal);
                }
            }

            return null;
        }

        private Tile GetDiscoveredWithLowestCostEstimate(HashSet<Tile> aCurrentlyDiscovered, Tile aGoal)
        {
            Tile closest = null;
            float minDistance = float.PositiveInfinity;

            foreach (Tile tile in aCurrentlyDiscovered)
            {
                if (HeuristicCostEstimate(tile, aGoal) < minDistance)
                {
                    closest = tile;
                    minDistance = HeuristicCostEstimate(tile, aGoal);
                }
            }

            return closest;
        }

        private float HeuristicCostEstimate(Tile aStart, Tile aGoal)
        {
            float distance = Vector2.Distance(aStart.Position, aGoal.Position);
            return distance / Tile.Size;
        }
        #endregion

    }
}
