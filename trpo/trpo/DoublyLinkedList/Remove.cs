using System;

namespace trpo.DoublyLinkedList
{
    internal class Remove<T> : IOperation<T>
    {
        public Node<T> TargetNode { get; }

        public void Transform()
        {
            throw new NotImplementedException();
        }

        public void InverseTransform()
        {
            throw new NotImplementedException();
        }
    }
}