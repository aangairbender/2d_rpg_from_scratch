using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Math;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Pointer
{
    public class PointerNode : TransformNode
    {
        public override void Accept(INodeVisitor visitor)
        {
            if (visitor is PointerVisitor pointerVisitor)
                pointerVisitor.Visit(this);
            else
                visitor.Visit(this);
        }
    }
}
