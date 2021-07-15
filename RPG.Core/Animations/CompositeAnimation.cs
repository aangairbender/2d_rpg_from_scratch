using System.Collections.Generic;
using System.Linq;

namespace RPG.Core.Animations
{
    public class CompositeAnimation<T> : AnimationBase<T>
    {
        private readonly List<AnimationBase<T>> _animations;

        private int _currentAnimationIndex;

        public CompositeAnimation(List<AnimationBase<T>> animations)
        {
            _animations = animations;
            foreach (var animation in _animations)
            {
                animation.ValueChanged += ChangeValue;
                animation.Started += Animation_Started;
                animation.Stopped += Animation_Stopped;
            }
            Duration = _animations.Sum(a => a.Duration);
        }

        private void Animation_Stopped()
        {
            _currentAnimationIndex++;
            if (_currentAnimationIndex == _animations.Count)
                return;
            
            _animations[_currentAnimationIndex].Start();
        }

        private void Animation_Started()
        {

        }

        public override void Start()
        {
            _currentAnimationIndex = 0;
            _animations[_currentAnimationIndex].Start();
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            _animations[_currentAnimationIndex].Stop();
        }

        public override void Update(float deltaTime)
        {
            _animations[_currentAnimationIndex].Update(deltaTime);
        }

        public override T CalculateValueFor(float time)
        {
            float curTime = 0;
            foreach (var animation in _animations)
            {
                var timePart = animation.Duration / this.Duration;

                if (curTime + timePart > time)
                    return animation.CalculateValueFor((time - curTime) / timePart);

                curTime += timePart;
            }

            return default(T);
        }
    }
}