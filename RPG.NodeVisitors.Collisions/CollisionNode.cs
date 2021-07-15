using System;
using RPG.Core;
using RPG.Math;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Collisions
{
    public class CollisionNode : TransformNode
    {
        public Shape Shape { get; set; }

        public bool IsTrigger { get; set; }
        public bool IsStatic { get; set; }

        public event Action<ISceneControl> TriggerEnter;
        public event Action<ISceneControl> TriggerLeave;

        internal void RaiseTriggerEnter(ISceneControl control)
        {
            TriggerEnter?.Invoke(control);
        }

        internal void RaiseTriggerLeave(ISceneControl control)
        {
            TriggerLeave?.Invoke(control);
        }

        public CollisionNode(Shape shape)
        {
            Shape = shape;
        }

        public override void Accept(INodeVisitor visitor)
        {
            if (visitor is CollisionVisitor collisionVisitor)
                collisionVisitor.Visit(this);
            else
                visitor.Visit(this);
        }
    }
}