using System;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class SScared : IGhostState
    {
        public void Enter(Ghost aGhost)
        {
            Console.ForegroundColor = aGhost.Color;
            Console.WriteLine("Entered SCARED");
        }

        public void Execute(Ghost aGhost)
        {
            if (aGhost.CollisionWithPlayer())
            {
                aGhost.ChangeState(new SDead());
            }

            if (aGhost.Player.PowerUp != PowerUpType.GhostEater)
            {
                aGhost.ChangeState(new SAlive());
            }
        }

        public void ExecuteGraphics(Ghost aGhost)
        {
            aGhost.FrameYIndex = 4;

            switch (aGhost.FrameXIndex)
            {
                case 0:
                case 1:
                case 2:
                    aGhost.FrameXIndex++;
                    break;
                case 3:
                default:
                    aGhost.FrameXIndex = 0;
                    break;
            }
        }

        public void Exit(Ghost aGhost)
        {
            Console.ForegroundColor = aGhost.Color;
            Console.WriteLine("Exited SCARED");
        }

        public Vector2? FindPath(Ghost aGhost)
        {
            return aGhost.AvoidPosition(aGhost.Player.Position);
        }
    }
}
