using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Graph
    {
        GameBoard myGameBoard;

        public Graph(GameBoard aGameBoard)
        {
            myGameBoard = aGameBoard;
        }
        
        public List<Tile> GetNeighbours(Tile aTile)
        {
            List<Tile> neighbours = new List<Tile>();
            
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row - 1, aTile.Column));
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row, aTile.Column - 1));
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row + 1, aTile.Column));
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row, aTile.Column + 1));

            neighbours = neighbours.FindAll(n => n != null);

            return neighbours;
        }

        public List<Tile> GetAllTiles()
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < myGameBoard.Rows; i++)
            {
                for (int k = 0; k < myGameBoard.Colums(i); k++)
                {
                    tiles.Add(myGameBoard.GetWalkableTile(i, k));
                }
            }

            tiles = tiles.FindAll(n => n != null);
            return tiles;
        }

        public float GetWeight(Tile aSource, Tile aDestination)
        {
            if (GetNeighbours(aSource).Contains(aDestination))
            {
                return 1;
            }
            return float.PositiveInfinity;
        }
    }
}
