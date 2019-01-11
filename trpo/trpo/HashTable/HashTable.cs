using System;
using System.Collections.Generic;
using System.Linq;
using trpo.ResizableArray;

namespace trpo.HashTable
{
    public class HashTable<T1, T2> : IPersistentHash<T1, T2> where T1 : IComparable where T2 : IComparable
    {
        private int currentVersionIndex;
        public Dictionary<T1, T2> hashTable;
        private readonly ResizableArray.LinkedList<IDictionary<T1, T2>> list;

        public HashTable(IDictionary<T1, T2> ienumerable)
        {
            list = new ResizableArray.LinkedList<IDictionary<T1, T2>>(currentVersionIndex, ienumerable);
            hashTable = new Dictionary<T1, T2>();
            foreach (var element in ienumerable)
                hashTable.Add(element.Key, element.Value);
        }

        public void Undo()
        {
            currentVersionIndex--;
            list.Downgrade();
            hashTable = list.Search(currentVersionIndex).Key.ToDictionary(k => k.Key, v => v.Value);
        }

        public void Redo()
        {
            currentVersionIndex++;
            list.Upgrade();
            hashTable = list.Search(currentVersionIndex).Key.ToDictionary(k => k.Key, v => v.Value);
        }

        public T2 this[T1 index]
        {
            get => hashTable[index];
            set => hashTable[index] = value;
        }

        public void Add(T1 key, T2 value)
        {
            hashTable.Add(key, value);
            list.Insert(new ListItem<IDictionary<T1, T2>>(++currentVersionIndex, hashTable));
        }

        public T2 Remove(T1 key)
        {
            var result = hashTable[key];
            hashTable.Remove(key);
            list.Insert(new ListItem<IDictionary<T1, T2>>(++currentVersionIndex, hashTable));
            return result;
        }

        public bool Contains(T1 key, T2 value)
        {
            foreach (var element in hashTable)
                if (element.Key.CompareTo(key) == 0)
                {
                    if (element.Value.CompareTo(value) == 0)
                    {
                        return true;
                    }

                    return false;
                }

            return false;
        }
    }
}