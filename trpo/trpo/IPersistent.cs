namespace trpo
{
    public interface IPersistent<T>
    {
        void Undo();
        void Redo();

        T this[int index] { get; set; }

        void Insert(int index, T elem);

        T Remove(int index);
    }
}