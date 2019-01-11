namespace trpo.DoublyLinkedList
{
    internal static class DoublyLinkedListExtension
    {
        public static void InsertNode<T>(this DoublyLinkedList<T> list, Node<T> targetNode, int index)
        {
            var follower = list.Head.GetNodeAtIndex(index);

            var predecessor = follower.Left;

            follower.Left = targetNode;

            if (predecessor != null)
            {
                predecessor.Right = targetNode;
            }

            targetNode.Left = predecessor;
            targetNode.Right = follower;

            if (targetNode.Left == null)
            {
                list.Head = targetNode;
            }
        }

        public static void RemoveNode<T>(this DoublyLinkedList<T> list, Node<T> TargetNode)
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