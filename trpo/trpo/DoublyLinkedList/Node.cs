namespace trpo.DoublyLinkedList
{
    internal class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T Value { get; set; }

        public override string ToString()
        {
            return $"Value = {Value}, Right = {{{Right}}}";
        }
    }
}