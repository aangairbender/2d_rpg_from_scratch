namespace RPG.Math.Shapes
{
    public class CircleShape : Shape
    {
        public Vector Center { get; set; }
        public float Radius { get; set; }

        public CircleShape(Vector center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        protected override BoundingBox CalculateBoundingBox()
        {
            return new BoundingBox(Center.X - Radius, Center.X + Radius, Center.Y - Radius, Center.Y + Radius);
        }

        public override void ApplyTransform(Matrix transform)
        {
            Center = Center.Transformed(transform);
            Radius *= MathHelper.Abs(transform.Scale);
        }

        public override Shape Clone()
        {
            return new CircleShape(Center, Radius);
        }
    }
}