namespace RPG.Core.Mapping
{
    public interface IHasFor
    {
        IHasUse<T> For<T>() where T : IAspect;
    }
}