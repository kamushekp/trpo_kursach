namespace trpo
{
    public interface IPersistentHash<T1, T2>
    {
        void Undo();

        void Redo();

        T2 this[T1 index] { get; set; }
        
        void Add(T1 key, T2 value);

        T2 Remove(T1 key);
    }
}