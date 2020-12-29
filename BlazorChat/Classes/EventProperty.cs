using System;

namespace BlazorChat.Classes
{
    public class EventProperty<T>
    {
        public EventProperty(T startValue = default(T))
        {
            this.value = startValue;
        }

        public event Action<T, T> ValueChanged;

        public T Value
        {
            get => value;
            set
            {
                var oldvalue = this.value;
                this.value = value;

                ValueChanged?.Invoke(oldvalue, value);
            }
        }
        private T value = default(T);
    }
}
