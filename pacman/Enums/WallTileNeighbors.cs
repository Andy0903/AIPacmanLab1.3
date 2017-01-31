using System;

namespace Pacman
{
    [Flags]
    enum WallTileNeighbors
    {
        Empty = 0,

        North = 1,
        South = 2,
        East = 4,
        West = 8,
    }
}
