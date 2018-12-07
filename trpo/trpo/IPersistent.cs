namespace trpo
{
    public interface IPersistent<T>
    {
        void Undo();
        void Redo();

        T this[int index] { get; set; }

        void Add(T elem, int index);

        T Remove(int index);
    }
}