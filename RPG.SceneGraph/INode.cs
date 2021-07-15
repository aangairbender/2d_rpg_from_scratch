namespace RPG.SceneGraph
{
    public interface INode
    {
        void Accept(INodeVisitor visitor);
    }
}
