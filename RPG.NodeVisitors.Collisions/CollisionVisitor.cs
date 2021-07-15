using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Math;
using RPG.Math.Shapes;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Collisions
{
    public class CollisionVisitor : INodeVisitor
    {
        private Matrix _transform = Matrix.Identity;
        private readonly List<Tuple<Shape, CollisionNode>> _shapes = new List<Tuple<Shape, CollisionNode>>();
        private readonly Dictionary<CollisionNode, Vector> _previousTranslate = new Dictionary<CollisionNode, Vector>();

        private readonly Dictionary<CollisionNode, HashSet<CollisionNode>> _collisions =
            new Dictionary<CollisionNode, HashSet<CollisionNode>>();
        private int _depth = 0;

        public void Reset()
        {
            _transform = Matrix.Identity;
            _depth = 0;
            _shapes.Clear();
        }

        public void Visit(Node node)
        {
            _depth++;
            foreach (var nodeChild in node.Children)
            {
                nodeChild.Accept(this);
            }

            _depth--;
            if (_depth == 0)
                CheckCollisions();
        }

        public void Visit(SwitchNode node)
        {
            if (!node.Enabled)
                return;
            Visit(node as Node);
        }

        public void Visit(TransformNode node)
        {
            var oldTransform = _transform.Clone();
            _transform *= node.Transform;
            Visit(node as Node);
            _transform = oldTransform;
        }

        public void Visit(CollisionNode node)
        {
            var shape = node.Shape.Clone();
            shape.ApplyTransform(_transform);
            shape.ApplyTransform(node.Transform);
            _shapes.Add(new Tuple<Shape, CollisionNode>(shape, node));
            Visit(node as TransformNode);
        }

        private void CheckCollisions()
        {
            foreach (var shape in _shapes)
            {
                if (_collisions.ContainsKey(shape.Item2))
                    continue;
                _collisions.Add(shape.Item2, new HashSet<CollisionNode>());
            }

            for (var i = 0; i < _shapes.Count; ++i)
            for (var j = i + 1; j < _shapes.Count; ++j)
            {
                var shapeA = _shapes[i].Item1;
                var nodeA = _shapes[i].Item2;
                var shapeB = _shapes[j].Item1;
                var nodeB = _shapes[j].Item2;

                // bounding boxes now are rotated so don't work
                //if (!BoundingBox.CollisionExist(shapeA.BoundingBox, shapeB.BoundingBox))
                //    continue;

                var wereColliding = _collisions[nodeA].Contains(nodeB);
                var nowColliding = shapeA.CollidesWith(shapeB);

                if (wereColliding && !nowColliding)
                {
                    _collisions[nodeA].Remove(nodeB);
                    _collisions[nodeB].Remove(nodeA);
                    if (nodeA.IsTrigger) nodeA.RaiseTriggerLeave(nodeB.ParentControl);
                    if (nodeB.IsTrigger) nodeB.RaiseTriggerLeave(nodeA.ParentControl);
                }

                if (!wereColliding && nowColliding)
                {
                    if (nodeA.IsTrigger || nodeB.IsTrigger)
                    {
                        _collisions[nodeA].Add(nodeB);
                        _collisions[nodeB].Add(nodeA);
                        if (nodeA.IsTrigger) nodeA.RaiseTriggerEnter(nodeB.ParentControl);
                        if (nodeB.IsTrigger) nodeB.RaiseTriggerEnter(nodeA.ParentControl);
                    }
                    else
                    {
                        if (_previousTranslate.ContainsKey(nodeA))
                            nodeA.Transform *= Matrix.CreateTranslate(_previousTranslate[nodeA] - nodeA.Transform.Translation);
                        if (_previousTranslate.ContainsKey(nodeB))
                            nodeB.Transform *= Matrix.CreateTranslate(_previousTranslate[nodeB] - nodeB.Transform.Translation);
                        }
                }
            }

            foreach (var shape in _shapes)
            {
                if (_previousTranslate.ContainsKey(shape.Item2))
                    _previousTranslate[shape.Item2] = shape.Item2.Transform.Translation;
                else
                    _previousTranslate.Add(shape.Item2, shape.Item2.Transform.Translation);
            }
        }
    }
}