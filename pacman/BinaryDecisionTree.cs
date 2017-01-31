using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    interface INode
    {
        IGhostState TraverseMe();
    }

    class LeafNode : INode
    {
        IGhostState myState;

        public LeafNode(IGhostState aState)
        {
            myState = aState;
        }

        public IGhostState TraverseMe()
        {
            return myState;
        }
    }

    class BranchNode : INode
    {
        public INode myTrueChild;
        public INode myFalseChild;
        Func<bool> myCondition;

        public BranchNode(Func<bool> aCondition, INode aFalseChild, INode aTrueChild)
        {
            myCondition = aCondition;
            myTrueChild = aTrueChild;
            myFalseChild = aFalseChild;
        }

        public IGhostState TraverseMe()
        {
            return myCondition() ? myTrueChild.TraverseMe() : myFalseChild.TraverseMe();
        }
    }

    class BinaryDecisionTree
    {
        BranchNode myRoot;

        public BinaryDecisionTree(Ghost aGhost)
        {
            ///               Not Dead
            ///              /         \
            ///            No           Yes
            ///           /               \
            ///     Is in spawn?      Pacman powered up?
            ///      /     \               /         \
            ///     No      Yes           No          Yes
            ///     /         \           /             \
            /// [SDead]    [SAlive]  [SAlive]     Collides with pacman?
            ///                                        /      \
            ///                                       No      Yes
            ///                                       /         \
            ///                                  [SScared]     [SDead]

            myRoot = new BranchNode(() => aGhost.CurrentBehaviour is SDead == false,
                        new BranchNode(() => aGhost.Position == aGhost.SpawnPosition,
                            new LeafNode(new SDead()),
                            new LeafNode(new SAlive())),

                        new BranchNode(() => aGhost.Player.PowerUp == PowerUpType.GhostEater,
                            new LeafNode(new SAlive()),
                            new BranchNode(() => aGhost.CollisionWithPlayer(),
                                new LeafNode(new SScared()),
                                new LeafNode(new SDead()))));
        }

        public IGhostState Traverse()
        {
            return myRoot.TraverseMe();
        }
    }
}
