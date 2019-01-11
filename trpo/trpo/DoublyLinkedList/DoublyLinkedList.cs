using System;
using System.Collections.Generic;

namespace trpo.DoublyLinkedList
{
    public class DoublyLinkedList<T> : IPersistent<T>
    {
        private readonly List<IOperation<T>> operations = new List<IOperation<T>>();
        private int lastOperation = -1;

        public DoublyLinkedList(T value)
        {
            Head = new Node<T>(value);
        }

        internal Node<T> Head { get; set; }

        public void Undo()
        {
            operations[lastOperation].InverseTransform();

            lastOperation--;
        }

        public void Redo()
        {
            lastOperation++;
            operations[lastOperation].Transform();
        }

        public T this[int index]
        {
            get => Head.GetNodeAtIndex(index).Value;
            set => throw new NotImplementedException();
        }

        public void Insert(int index, T elem)
        {
            var node = new Node<T>(elem);
            var operation = new Insert<T>(node, index, this);
            operation.Transform();

            if (index == 0)
            {
                Head = node;
            }

            operations.Add(operation);
            lastOperation++;
        }

        public T Remove(int index)
        {
            throw new NotImplementedException();
        }
    }
}