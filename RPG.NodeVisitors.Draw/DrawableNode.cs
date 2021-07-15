using RPG.NodeVisitors.Draw;

namespace RPG.SceneGraph
{
    public class DrawableNode : INode
    {
        public IDrawable Drawable { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            if (visitor is DrawVisitor drawVisitor)
                drawVisitor.Visit(this);
        }
    }
}