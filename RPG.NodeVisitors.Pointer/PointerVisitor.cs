using System;
using RPG.Math;
using RPG.NodeVisitors.Draw;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Pointer
{
    public class PointerVisitor : INodeVisitor
    {
        private Matrix _pointerTransform;
        private readonly Camera _camera;

        public Vector Pointer { get; set; }

        public PointerVisitor(Camera camera)
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
            if (!node.Enabled)
                return;
            Visit(node as Node);
        }

        public void Visit(TransformNode node)
        {
            _pointerTransform *= node.Transform.Inverted();
            Visit(node as Node);
            _pointerTransform *= node.Transform;
        }

        public void Reset()
        {
            _pointerTransform = _camera.Transform * Matrix.CreateTranslate(Pointer);
        }

        public void Visit(PointerNode node)
        {
            node.Transform = _pointerTransform.Clone();
        }
    }
}