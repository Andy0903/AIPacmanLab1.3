using Microsoft.Xna.Framework;
using System;

namespace Pacman
{
    class SDead : IGhostState
    {
        public void Enter(Ghost aGhost)
        {
            Console.ForegroundColor = aGhost.Color;
            Console.WriteLine("Entered DEAD");
            SoundEffectManager.PlayGhostSound();
        }

        public void Execute(Ghost aGhost)
        {
            if (aGhost.Position == aGhost.SpawnPosition)
            {
                aGhost.ChangeState(new SAlive());
            }
        }

        public void ExecuteGraphics(Ghost aGhost)
        {
            aGhost.FrameYIndex = 4;
            switch (aGhost.Direction)
            {
                case Direction.Up:
                    if (aGhost.FrameXIndex != 6)
                    {
                        aGhost.FrameXIndex = 6;
                    }
                    break;
                case Direction.Left:
                    if (aGhost.FrameXIndex != 5)
                    {
                        aGhost.FrameXIndex = 5;
                    }
                    break;
                case Direction.Down:
                    if (aGhost.FrameXIndex != 7)
                    {
                        aGhost.FrameXIndex = 7;
                    }
                    break;
                case Direction.Right:
                    if (aGhost.FrameXIndex != 4)
                    {
                        aGhost.FrameXIndex = 4;
                    }
                    break;
            }
        }

        public void Exit(Ghost aGhost)
        {
            Console.ForegroundColor = aGhost.Color;
            Console.WriteLine("Exited DEAD");
        }

        public Vector2? FindPath(Ghost aGhost)
        {
            return aGhost.GoToPosition((int)aGhost.SpawnPosition.X / Tile.Size, (int)aGhost.SpawnPosition.Y / Tile.Size);
        }
    }
}
