using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Math.Shapes
{
    public class CompoundShape : Shape
    {
        public List<Shape> Shapes { get; }

        public CompoundShape(List<Shape> shapes)
        {
            Shapes = shapes;
        }

        protected override BoundingBox CalculateBoundingBox()
        {
            var result = new BoundingBox();

            return Shapes.Aggregate(result, (current, shape) => BoundingBox.Merge(current, shape.BoundingBox));
        }

        public override void ApplyTransform(Matrix transform)
        {
            foreach (var shape in Shapes)
            {
                shape.ApplyTransform(transform);
            }
        }

        public override Shape Clone()
        {
            return new CompoundShape(Shapes);
        }
    }
}