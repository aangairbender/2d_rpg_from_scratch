namespace RPG.Math
{
    public struct BoundingBox
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;

        public BoundingBox(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public bool Contains(Vector point)
        {
            return (Left <= point.X && point.X <= Right &&
                    Top <= point.Y && point.Y <= Bottom);
        }

        public static BoundingBox Merge(BoundingBox boxA, BoundingBox boxB)
        {
            return new BoundingBox(
                MathHelper.Min(boxA.Left, boxB.Left),
                MathHelper.Max(boxA.Right, boxB.Right),
                MathHelper.Min(boxA.Top, boxB.Top),
                MathHelper.Max(boxA.Bottom, boxB.Bottom));
        }

        public static bool CollisionExist(BoundingBox boxA, BoundingBox boxB)
        {
            return ProjectionCollisionExist(boxA.Left, boxA.Right, boxB.Left, boxB.Right) ||
                   ProjectionCollisionExist(boxA.Top, boxA.Bottom, boxB.Top, boxB.Bottom);
        }

        private static bool ProjectionCollisionExist(float leftA, float rightA, float leftB, float rightB)
        {
            return (leftA <= leftB && leftB <= rightA) ||
                   (leftB <= leftA && leftA <= rightB);
        }
    }
}