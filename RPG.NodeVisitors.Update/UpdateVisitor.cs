using RPG.SceneGraph;

namespace RPG.NodeVisitors.Update
{
    public class UpdateVisitor : INodeVisitor
    {
        public float DeltaTime { get; set; }

        public void Visit(Node node)
        {
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }
        }

        public void Visit(SwitchNode node)
        {
            if (!node.Enabled)
                return;
            Visit(node as Node);
        }

        public void Visit(TransformNode node)
        {
            Visit(node as Node);
        }

        public void Reset()
        {
            //
        }

        public void Visit(UpdateNode node)
        {
            node.UpdateAction?.Invoke(DeltaTime);
        }
    }
}