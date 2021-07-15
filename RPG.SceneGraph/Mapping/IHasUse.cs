using System;

namespace RPG.Core.Mapping
{
    public interface IHasUse<TFor> where TFor : IAspect
    {
        IHasFor Use<T>(Func<TFor, T> generator) where T : SceneControl<TFor>;
        IHasFor Use<T>(Func<TFor, ICreator, T> generator) where T : SceneControl<TFor>;
    }
}