using System.Collections.Generic;
using System.Linq;

namespace trpo.ResizableArray
{
    public class ResizableArray<T> : IPersistent<T>
    {
        private int currentVersionIndex;
        private readonly LinkedList<IEnumerable<T>> list;
        public List<T> resizableArray;

        public ResizableArray() : this(new T[0])
        {
        }

        public ResizableArray(IEnumerable<T> ienumerable)
        {
            list = new LinkedList<IEnumerable<T>>(currentVersionIndex, ienumerable);
            resizableArray = new List<T>();
            foreach (var element in ienumerable)
                resizableArray.Add(element);
        }

        public void Undo()
        {
            currentVersionIndex--;
            list.Downgrade();
            resizableArray = list.Search(currentVersionIndex).Key.ToList();
        }

        public void Redo()
        {
            currentVersionIndex++;
            list.Upgrade();
            resizableArray = list.Search(currentVersionIndex).Key.ToList();
        }

        public T this[int index]
        {
            get => resizableArray[index];
            set => resizableArray[index] = value;
        }

        public void Insert(int index, T elem)
        {
            resizableArray.Insert(index, elem);
            list.Insert(new ListItem<IEnumerable<T>>(++currentVersionIndex, resizableArray));
        }

        public T Remove(int index)
        {
            var result = resizableArray[index];
            resizableArray.RemoveAt(index);
            list.Insert(new ListItem<IEnumerable<T>>(++currentVersionIndex, resizableArray));
            return result;
        }
    }
}