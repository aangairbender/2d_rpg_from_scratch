namespace RPG.Math.Shapes
{
    public class LineShape : Shape
    {
        public Vector PointA { get; set; }
        public Vector PointB { get; set; }

        public LineShape(Vector pointA, Vector pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }

        protected override BoundingBox CalculateBoundingBox()
        {
            return new BoundingBox(
                MathHelper.Min(PointA.X, PointB.X),
                MathHelper.Max(PointA.X, PointB.X),
                MathHelper.Min(PointA.Y, PointB.Y),
                MathHelper.Max(PointA.Y, PointB.Y));
        }

        public override void ApplyTransform(Matrix transform)
        {
            PointA = PointA.Transformed(transform);
            PointB = PointB.Transformed(transform);
        }

        public override Shape Clone()
        {
            return new LineShape(PointA, PointB);
        }
    }
}