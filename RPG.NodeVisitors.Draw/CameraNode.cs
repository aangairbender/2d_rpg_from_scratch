using RPG.SceneGraph;

namespace RPG.NodeVisitors.Draw
{
    public class CameraNode : TransformNode
    {
        public Camera Camera { get; internal set; }

        public override void Accept(INodeVisitor visitor)
        {
            if (visitor is CameraVisitor cameraVisitor)
                cameraVisitor.Visit(this);
            else
                visitor.Visit(this);
        }
    }
}