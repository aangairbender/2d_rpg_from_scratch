using System;
using System.Collections.Generic;

namespace RPG.Core.Mapping
{
    public class Mapper : IHasFor, ICreator
    {
        private readonly Dictionary<Type, Func<IAspect, ISceneControl>> _bindings =
            new Dictionary<Type, Func<IAspect, ISceneControl>>();

        public IHasUse<T> For<T>() where T : IAspect
        {
            return new Binding<T>(this);
        }

        internal void AddBinding<TA, TSC>(Func<TA, ICreator, TSC> generator)
            where TA : IAspect
            where TSC : SceneControl<TA>
        {
            _bindings.Add(typeof(TA), (a) => generator((TA) a, this)); 
        }

        internal void AddBinding<TA, TSC>(Func<TA, TSC> generator)
            where TA : IAspect
            where TSC : SceneControl<TA>
        {
            _bindings.Add(typeof(TA), (a) => generator((TA)a));
        }

        public ISceneControl CreateFor<T>(T aspect) where T : IAspect
        {
            return _bindings[typeof(T)]?.Invoke(aspect);
        }
    }
}