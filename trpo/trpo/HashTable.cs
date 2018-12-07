namespace trpo
{
    public class HashTable<T> : IPersistent<T>
    {
        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        public void Redo()
        {
            throw new System.NotImplementedException();
        }

        public T this[int index]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public void Add(T elem, int index)
        {
            throw new System.NotImplementedException();
        }

        public T Remove(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}