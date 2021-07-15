namespace RPG.Core.Mapping
{
    public interface ICreator
    {
        ISceneControl CreateFor<T>(T aspect) where T : IAspect;
    }
}