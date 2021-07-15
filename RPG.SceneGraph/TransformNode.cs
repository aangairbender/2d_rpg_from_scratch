using RPG.Math;

namespace RPG.SceneGraph
{
    public class TransformNode : Node
    {
        public Matrix Transform { get; set; } = Matrix.Identity;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}