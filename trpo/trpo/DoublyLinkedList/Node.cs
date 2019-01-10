using System;
using System.Collections.Generic;

namespace trpo.DoublyLinkedList
{
    internal class Node<T>
    {
        private int currentVersionIndex;

        public Node(Guid version) : this(version, default(T))
        {
        }

        public Node(Guid version, T value) : this(version, value, null, null)
        {
        }

        public Node(Guid version, T value, Node<T> left, Node<T> right)
        {
            AvailableVersions = new List<Guid> {version};
            Lefts = new Dictionary<Guid, Node<T>> {{CurrentVersion, left}};
            Rights = new Dictionary<Guid, Node<T>> {{CurrentVersion, right}};
            Values = new Dictionary<Guid, T> {{CurrentVersion, value}};
        }

        private List<Guid> AvailableVersions { get; }
        private Dictionary<Guid, Node<T>> Lefts { get; }
        private Dictionary<Guid, Node<T>> Rights { get; }
        private Dictionary<Guid, T> Values { get; }

        private Guid CurrentVersion => AvailableVersions[currentVersionIndex];

        public Node<T> Left
        {
            get => Lefts[CurrentVersion];
            set => Lefts[CurrentVersion] = value;
        }

        public Node<T> Right
        {
            get => Rights[CurrentVersion];
            set => Rights[CurrentVersion] = value;
        }

        public T Value
        {
            get => Values[CurrentVersion];
            set => Values[CurrentVersion] = value;
        }

        public void Upgrade()
        {
            if (currentVersionIndex + 1 < AvailableVersions.Count)
            {
                currentVersionIndex++;
            }
        }

        public void Downgrade()
        {
            if (currentVersionIndex - 1 >= 0)
            {
                currentVersionIndex--;
            }
        }

        public void AddNewVersion(Guid version, T value, Node<T> left, Node<T> right)
        {
            AvailableVersions.Add(version);
            currentVersionIndex++;
            this.Values[CurrentVersion] = value;
            this.Lefts[CurrentVersion] = left;
            this.Rights[CurrentVersion] = right;
        }
    }
}