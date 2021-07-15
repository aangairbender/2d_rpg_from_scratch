using RPG.SceneGraph;

namespace RPG.NodeVisitors.Input
{
    public class InputNode : INode
    {
        public InputState InputState { get; internal set; }

        public void Accept(INodeVisitor visitor)
        {
            if (visitor is InputVisitor inputVisitor)
                inputVisitor.Visit(this);
        }
    }
}