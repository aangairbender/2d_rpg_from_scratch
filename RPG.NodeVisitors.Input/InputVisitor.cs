using RPG.SceneGraph;

namespace RPG.NodeVisitors.Input
{
    public class InputVisitor : INodeVisitor
    {
        private readonly InputState _inputState;

        public InputVisitor(InputState inputState)
        {
            _inputState = inputState;
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

        public void Visit(InputNode node)
        {
            node.InputState = _inputState;
        }
    }
}
