namespace trpo.DoublyLinkedList
{
    internal class Insert<T> : IOperation<T>
    {
        private readonly int index;
        private readonly DoublyLinkedList<T> list;

        public Insert(Node<T> targetNode, int index, DoublyLinkedList<T> list)
        {
            TargetNode = targetNode;
            this.index = index;
            this.list = list;
        }

        public Node<T> TargetNode { get; }

        public void Transform()
        {
            var follower = list.Head.GetNodeAtIndex(index);

            var predecessor = follower.Left;

            follower.Left = TargetNode;

            if (predecessor != null)
            {
                predecessor.Right = TargetNode;
            }

            TargetNode.Left = predecessor;
            TargetNode.Right = follower;

            if (TargetNode.Left == null)
            {
                list.Head = TargetNode;
            }
        }

        public void InverseTransform()
        {
            var predecessor = TargetNode.Left;
            var follower = TargetNode.Right;

            if (predecessor != null)
            {
                predecessor.Right = follower;
            }

            follower.Left = predecessor;
            if (follower.Left == null)
            {
                list.Head = follower;
            }
        }
    }
}