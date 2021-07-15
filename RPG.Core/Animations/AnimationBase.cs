using System;

namespace RPG.Core.Animations
{
    public abstract class AnimationBase<T>
    {
        private float _curTime;

        public float Duration { get; set; }

        public abstract T CalculateValueFor(float time);

        public event Action<T> ValueChanged;
        public bool Running { get; private set; }

        public event Action Started;
        public event Action Stopped;

        public virtual void Start()
        {
            _curTime = 0;
            Running = true;
            Started?.Invoke();
        }

        public virtual void Stop()
        {
            Running = false;
            Stopped?.Invoke();
        }

        public virtual void Update(float deltaTime)
        {
            if (_curTime >= Duration)
                Stop();

            if (!Running)
                return;
            
            _curTime += deltaTime;
            ChangeValue(CalculateValueFor(_curTime / Duration));
        }

        protected virtual void ChangeValue(T value)
        {
            ValueChanged?.Invoke(value);
        }
    }
}