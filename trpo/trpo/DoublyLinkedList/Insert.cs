namespace trpo.DoublyLinkedList
{
    internal class Insert<T> :IOperation<T>
    {
        private int index;
        private Node<T> head;
        public Insert(Node<T> targetNode, Node<T> head, int index)
        {
            TargetNode = targetNode;
            this.index = index;
            this.head = head;
        }

        public Node<T> TargetNode { get; }
        public void Transform()
        { 
            var follower = head.GetNodeAtIndex(this.index);

            var predecessor = follower.Left;

            follower.Left = TargetNode;

            if (predecessor != null)
            {
                predecessor.Right = TargetNode;
            }

            TargetNode.Left = predecessor;
            TargetNode.Right = follower;
        }

        public void InverseTransform(ref Node<T> head)
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
                head = follower;
            }
        }
    }
}