namespace trpo.DoublyLinkedList
{
    internal class Remove<T> : IOperation<T>
    {
        private readonly int index;
        private readonly DoublyLinkedList<T> list;

        public Remove(Node<T> targetNode, int index, DoublyLinkedList<T> list)
        {
            TargetNode = targetNode;
            this.index = index;
            this.list = list;
        }

        public Node<T> TargetNode { get; }

        public void InverseTransform()
        {
            list.InsertNode(TargetNode, index);
        }

        public void Transform()
        {
            list.RemoveNode(TargetNode);
        }
    }
}