using trpo.Properties;

namespace trpo
{
    internal class OperationInfo<T>
    {
        public Operation Operation { get; }

        public T Elem { get; }
    }
}