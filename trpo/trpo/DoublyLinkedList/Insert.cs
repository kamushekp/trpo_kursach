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
            list.InsertNode(TargetNode, index);
        }

        public void InverseTransform()
        {
            list.RemoveNode(TargetNode);
        }
    }
}