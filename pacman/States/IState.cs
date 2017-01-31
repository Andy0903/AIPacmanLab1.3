using Microsoft.Xna.Framework;

namespace Pacman
{
    interface IGhostState
    {
        void Enter(Ghost aGhost);
        Vector2? FindPath(Ghost aGhost);
        //void Execute(Ghost aGhost);
        void Execute(Ghost aGhost);
        void Exit(Ghost aGhost);
    }
}
