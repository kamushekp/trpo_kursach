using System.Collections.Generic;

namespace trpo.ResizableArray
{
    public class ListItem<T>
    {
        public ListItem(int version, T key)
        {
            Key = key;
            currentVersion = version;
        }

        public int currentVersion { get; set; }
        public T Key { get; set; }
        public ListItem<T> Next { get; set; }
        public ListItem<T> Prev { get; set; }
    }

    public class LinkedList<T>
    {
        private int currentVersionIndex;

        public LinkedList(int version, T key)
        {
            AvailableVersions = new List<int> {version};
            head = new ListItem<T>(version, key);
        }

        public ListItem<T> head { get; private set; }
        private List<int> AvailableVersions { get; }

        public void Insert(ListItem<T> item)
        {
            item.Next = head;
            item.Prev = null;

            if (head != null)
            {
                head.Prev = item;
            }

            head = item;
            currentVersionIndex++;
        }

        public void Delete(ListItem<T> item)
        {
            if (item.Prev != null)
            {
                item.Prev.Next = item.Next;
            }
            else
            {
                head = item.Next;
            }

            if (item.Next != null)
            {
                item.Next.Prev = item.Prev;
            }

            currentVersionIndex--;
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

        public ListItem<T> Search(int index)
        {
            var item = head;
            while (item != null && item.currentVersion != index)
                item = item.Next;

            return item;
        }
    }
}