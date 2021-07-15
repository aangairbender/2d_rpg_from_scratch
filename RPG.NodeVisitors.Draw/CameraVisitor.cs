using System;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Draw
{
    public class CameraVisitor : INodeVisitor
    {
        private readonly Camera _camera;

        public CameraVisitor(Camera camera)
        {
            _camera = camera;
        }

        public void Visit(Node node)
        {
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }
        }

        public void Visit(SwitchNode node)
        {
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

        public void Visit(CameraNode node)
        {
            node.Camera = _camera;
        }
    }
}