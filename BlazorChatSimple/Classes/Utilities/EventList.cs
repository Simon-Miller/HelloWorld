using System;
using System.Collections;
using System.Collections.Generic;

namespace BlazorChatSimple.Classes.Utilities
{
    /// <summary>
    /// List of any class type, where a null value is not considered a value to be stored.  Setting an existing value to null will remove it from the list.
    /// </summary>
    public class EventList<T> : IEnumerable<T>, IReadOnlyCollection<T>
        where T : class
    {
        public event Action<IReadOnlyCollection<T>> ListChanged;
        private List<T> list = new List<T>();

        public int Count => list.Count;

        public void Add(T item)
        {
            list.Add(item);
            ListChanged?.Invoke(list);
        }

        public void Remove(T item)
        {
            list.Remove(item);
            ListChanged?.Invoke(list);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T this[int i]
        {
            get => list[i];
            set
            {
                if (value is null)
                    list.RemoveAt(i);
                else
                    list[i] = value;

                ListChanged?.Invoke(list);
            }
        }
    }
}
