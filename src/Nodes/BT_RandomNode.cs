using System;

namespace Common.BehaviourTrees
{
    /// <summary>
    /// <see cref="BT_ANode"/> which executes a random child task as long as it is running
    /// </summary>
    public sealed class BT_RandomNode : BT_ANode
    {
        private readonly Random _random;

        public BT_RandomNode() :
            this(null)
        {
        }

        public BT_RandomNode(Random random = null) :
            this("Random", random)
        {
        }

        public BT_RandomNode(string name, Random random = null) :
            base(name)
        {
            _random = random ?? new Random();
        }
        
        protected override void OnStart()
        {
            _current = _random.Next(0, _children.Length);
        }

        protected override BT_EStatus OnUpdate()
        {
            if (_current < _children.Length)
            {
                var current = _children[_current];
                return current.Update();
            }

            return BT_EStatus.Failure;
        }
    }
}
