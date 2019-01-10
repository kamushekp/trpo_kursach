using System;
using System.Collections.Generic;
using System.Linq;

namespace trpo.DoublyLinkedList
{
    public class DoublyLinkedList<T> : IPersistent<T>
    {
        private readonly Guid currentVersion;
        private readonly Node<T> head;

        public DoublyLinkedList() : this(new T[0])
        {
        }

        public DoublyLinkedList(IEnumerable<T> ienumerable)
        {
            currentVersion = Guid.NewGuid();
            head = new Node<T>(currentVersion);
            head.Value = ienumerable.FirstOrDefault();

            var current = head;

            foreach (var value in ienumerable.Skip(1))
            {
                var newNode = new Node<T>(currentVersion);
                newNode.Value = value;
                current.Right = newNode;
                current = newNode;
            }
        }

        public void Undo()
        {
            ApplyForNodes(node => node.Downgrade());
        }

        public void Redo()
        {
            ApplyForNodes(node => node.Upgrade());
        }

        public T this[int index]
        {
            get => FindNodeByIndex(index).Value;
            set => FindNodeByIndex(index).Value = value;
        }

        public void Insert(int index, T elem)
        {
            var willBeBehind = FindNodeByIndex(index);
            var willBeAhead = willBeBehind.Right;

            var newVersion = new Guid();
            var newNode = new Node<T>(newVersion, elem, willBeBehind, willBeAhead);

            willBeBehind.AddNewVersion(newVersion, willBeBehind.Value, willBeBehind.Left, newNode);
            willBeAhead?.AddNewVersion(newVersion, willBeAhead.Value, newNode, willBeAhead.Right);
        }

        public T Remove(int index)
        {
            var toRemove = FindNodeByIndex(index);

            var newVersion = new Guid();
            var nodeFromLeft = toRemove.Left;
            var nodeFromRight = toRemove.Right;

            nodeFromLeft.AddNewVersion(newVersion, nodeFromLeft.Value, nodeFromLeft.Left, nodeFromRight);
            nodeFromRight.AddNewVersion(newVersion, nodeFromRight.Value, nodeFromLeft, nodeFromRight.Right);

            return toRemove.Value;
        }

        private void ApplyForNodes(Action<Node<T>> action)
        {
            var current = head;
            do
            {
                action(current);
                current = current.Right;

            } while (current != null);

        }

        private Node<T> FindNodeByIndex(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var current = head;

            for (var currentIndex = 0; currentIndex < index; currentIndex++)
            {
                current = current.Right;
                if (current == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
            }

            return current;
        }
    }
}