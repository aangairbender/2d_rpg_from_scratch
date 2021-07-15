namespace RPG.SceneGraph
{
    public interface INodeVisitor
    {
        void Visit(Node node);
        void Visit(SwitchNode node);
        void Visit(TransformNode node);

        void Reset();
    }
}