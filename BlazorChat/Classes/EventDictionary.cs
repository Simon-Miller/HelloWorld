using System;
using System.Collections.Generic;

namespace BlazorChat.Classes
{
    public class EventDictionary<K, V>
    {
        public event Action Changed;
        private Dictionary<K, V> dictionary = new Dictionary<K, V>();

        public IReadOnlyCollection<K> Keys => dictionary.Keys;

        public bool Add(K key, V value)
        {
            if (this.dictionary.ContainsKey(key) == false)
            {
                this.dictionary.Add(key, value);
                this.Changed?.Invoke();
                return true;
            }

            return false;
        }

        public bool Remove(K key)
        {
            if (this.dictionary.ContainsKey(key))
            {
                this.dictionary.Remove(key);
                this.Changed?.Invoke();
                return true;
            }

            return false;
        }

        public V this[K key]
        {
            get => this.dictionary[key];
            set
            {
                this.dictionary[key] = value;
                this.Changed?.Invoke();
            }
        }
    }
}
