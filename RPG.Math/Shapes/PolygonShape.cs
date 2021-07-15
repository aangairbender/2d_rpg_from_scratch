using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RPG.Math.Shapes
{
    public class PolygonShape : Shape
    {
        public List<Vector> Vertices { get; }

        public PolygonShape(List<Vector> vertices)
        {
            Vertices = vertices;
        }

        protected override BoundingBox CalculateBoundingBox()
        {
            return new BoundingBox(
                Vertices.Min(v => v.X),
                Vertices.Max(v => v.X),
                Vertices.Min(v => v.Y),
                Vertices.Max(v => v.Y));
        }

        public override void ApplyTransform(Matrix transform)
        {
            for (var i = 0; i < Vertices.Count; ++i)
            {
                Vertices[i] = Vertices[i].Transformed(transform);
            }
        }

        public override Shape Clone()
        {
            return new PolygonShape(Vertices);
        }
    }
}