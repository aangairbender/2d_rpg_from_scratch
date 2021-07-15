using System.Drawing;

namespace RPG.SceneGraph
{
    public class SwitchNode : Node
    {
        public bool Enabled { get; set; } = true;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}