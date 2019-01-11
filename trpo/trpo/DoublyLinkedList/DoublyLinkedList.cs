using System;
using System.Collections.Generic;
using System.Linq;

namespace trpo.DoublyLinkedList
{
    public class DoublyLinkedList<T> : IPersistent<T>
    {
        private Node<T> head;

        private List<IOperation<T>> operations = new List<IOperation<T>>();
        private int currentOperation = 0;

        public DoublyLinkedList(T value)
        {
            head = new Node<T>(value);
        }

        public void Undo()
        {
            for(var i = operations.Count - 1; i >= 0; i-- )
            {
                operations[i].InverseTransform(ref head);
            }
        }

        public void Redo()
        {
        }

        public T this[int index]
        {
            get { return head.GetNodeAtIndex(index).Value;}
            set { }
        }

        public void Insert(int index, T elem)
        {
            var node = new Node<T>(elem);
            var operation = new Insert<T>(node, head, index);
            operation.Transform();

            if (index == 0)
            {
                head = node;
            }

            operations.Add(operation);
        }

        public T Remove(int index)
        {
            throw  new NotImplementedException();
        }

    }
}