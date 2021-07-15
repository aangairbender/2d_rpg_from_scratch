using System.Drawing;
using System.Drawing.Drawing2D;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Draw
{
    public class DrawVisitor : INodeVisitor
    {
        private readonly Graphics _g;
        private readonly Camera _camera;

        public DrawVisitor(Graphics g, Camera camera)
        {
            _g = g;
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
            var matrix = new Matrix(
                node.Transform.M11, node.Transform.M12,
                node.Transform.M21, node.Transform.M22,
                node.Transform.M31, node.Transform.M32);

            var beforeDraw = _g.Transform.Clone();
            beforeDraw.Multiply(matrix);

            var afterDraw = _g.Transform.Clone();

            _g.Transform = beforeDraw;
            Visit(node as Node);
            _g.Transform = afterDraw;
        }

        public void Reset()
        {
            var cameraMatrix = new Matrix(
                _camera.Transform.M11, _camera.Transform.M12,
                _camera.Transform.M21, _camera.Transform.M22,
                _camera.Transform.M31, _camera.Transform.M32);
            cameraMatrix.Invert();
            _g.Transform = cameraMatrix;
            _g.Clear(Color.Black);
        }

        public void Visit(DrawableNode node)
        {
            node.Drawable.Draw(_g);
        }
    }
}