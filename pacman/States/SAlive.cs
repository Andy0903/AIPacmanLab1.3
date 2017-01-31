using Microsoft.Xna.Framework;
using System;

namespace Pacman
{
    class SAlive : IGhostState
    {
        public void Enter(Ghost aGhost)
        {
            Console.ForegroundColor = aGhost.Color;
            Console.WriteLine("Entered ALIVE");
        }

        public void Execute(Ghost aGhost)
        {
            if (aGhost.Player.PowerUp == PowerUpType.GhostEater)
            {
                aGhost.ChangeState(new SScared());
            }

            if (aGhost.CollisionWithPlayer())
            {
                aGhost.Player.GotEaten();
            }
        }

        public void ExecuteGraphics(Ghost aGhost)
        {
            aGhost.FrameYIndex = aGhost.DEFAULT_FRAME_Y_INDEX;
            switch (aGhost.Direction)
            {
                case Direction.Up:
                    aGhost.FrameXIndex = aGhost.FrameXIndex != 4 ? 4 : 5;
                    break;
                case Direction.Left:
                    aGhost.FrameXIndex = aGhost.FrameXIndex != 2 ? 2 : 3;
                    break;
                case Direction.Down:
                    aGhost.FrameXIndex = aGhost.FrameXIndex != 6 ? 6 : 7;
                    break;
                case Direction.Right:
                    aGhost.FrameXIndex = aGhost.FrameXIndex != 0 ? 0 : 1;
                    break;
            }
        }

        public void Exit(Ghost aGhost)
        {
            Console.ForegroundColor = aGhost.Color;
            Console.WriteLine("Exited ALIVE");
        }

        public Vector2? FindPath(Ghost aGhost)
        {      
           return aGhost.GoToPosition(aGhost.Player.Column, aGhost.Player.Row);
        }
    }
}
