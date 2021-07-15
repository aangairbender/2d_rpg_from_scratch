using System;

namespace RPG.Core.Mapping
{
    internal class Binding<TFor> : IHasUse<TFor> where TFor : IAspect
    {
        private readonly Mapper _mapper;

        public Binding(Mapper mapper)
        {
            _mapper = mapper;
        }

        public IHasFor Use<T>(Func<TFor, ICreator, T> generator) where T : SceneControl<TFor>
        {
            _mapper.AddBinding(generator);
            return _mapper;
        }

        public IHasFor Use<T>(Func<TFor, T> generator) where T : SceneControl<TFor>
        {
            _mapper.AddBinding(generator);
            return _mapper;
        }
    }
}