using System;

namespace RPG.Core
{
    public class Aspect<T> : IAspect
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }

        public event Action ValueChanged;

        public Aspect(T value)
        {
            _value = value;
        }
    }
}