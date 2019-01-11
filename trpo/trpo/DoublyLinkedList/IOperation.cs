namespace trpo.DoublyLinkedList
{
    internal interface IOperation<T>
    {
        Node<T> TargetNode { get; }
        void Transform();
        void InverseTransform(ref Node<T> head);
    }
}