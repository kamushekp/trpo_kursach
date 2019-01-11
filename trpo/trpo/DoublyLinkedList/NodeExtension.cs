using System;
using System.Collections.Generic;
using System.Linq;

namespace trpo.DoublyLinkedList
{
    internal static class NodeExtension
    {
        public static IEnumerable<Node<T>> EnumerateNodes<T>(this Node<T> head)
        {
            for (var node = head; ; node = head.Right)
            {
                yield return node;
                if (head.Right == null)
                {
                    yield break;
                }
            }
        }

        public static Node<T> GetNodeAtIndex<T>(this Node<T> head, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var node = head.EnumerateNodes().ElementAtOrDefault(index);
            if (node == null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return node;
        }
    }
}