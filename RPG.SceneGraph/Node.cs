using System;
using System.Collections.Generic;
using System.Drawing;
using RPG.Core;

namespace RPG.SceneGraph
{
    public class Node : INode
    {
        private readonly ICollection<INode> _children = new List<INode>();

        public IEnumerable<INode> Children => _children;

        public ISceneControl ParentControl { get; internal set; }

        public void AddChild(INode child)
        {
            _children.Add(child);
        }

        public void RemoveChild(INode child)
        {
            _children.Remove(child);
        }

        public virtual void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}