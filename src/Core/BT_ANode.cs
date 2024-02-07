namespace Common.BehaviourTrees
{
    /// <summary>
    /// <see cref="BT_ATask"/> node with a multiple child tasks support
    /// </summary>
    public abstract class BT_ANode : BT_ATask, BT_INode
    {
        protected BT_ITask[] _children;
        protected int _current;

        public BT_ITask[] Children
            => _children;

        public BT_ITask Current
            => _children[_current];

        public BT_ANode(string name = null) :
            base(name)
        {
        }

        public BT_ANode WithChild(BT_ITask child)
        {
            _children = new BT_ITask[] { child };
            return this;
        }

        public BT_ANode WithChildren(params BT_ITask[] children)
        {
            _children = children;
            return this;
        }
        
        protected override void OnStart()
        {
            _current = 0;
        }

        protected override void OnFinish()
        {
            AbortChildren();
        }

        protected void AbortChildren()
        {
            if (_children != null)
            {
                for (int i = 0; i < _children.Length; ++i)
                {
                    _children[i].Abort();
                }
            }
        }
    }
}
