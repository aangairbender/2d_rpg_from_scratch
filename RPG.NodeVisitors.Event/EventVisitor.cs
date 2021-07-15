using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Event
{
    public class EventVisitor : INodeVisitor
    {
        public EventVisitor()
        {

        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Visit(GroupNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(SwitchNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(TransformNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(DrawableNode node)
        {
            throw new NotImplementedException();
        }

        public void Visit(EventNode node)
        {
            throw new NotImplementedException();
        }
    }
}
