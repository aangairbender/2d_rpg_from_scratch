using System;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Update
{
    public class UpdateNode : INode
    {
        public Action<float> UpdateAction { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            if (visitor is UpdateVisitor updateVisitor)
                updateVisitor.Visit(this);
        }
    }
}