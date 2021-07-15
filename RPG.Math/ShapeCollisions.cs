using System;
using System.Linq;
using RPG.Math.Shapes;

namespace RPG.Math
{
    public static class ShapeCollisions
    {
        public static bool CollidesWith(this Shape a, Shape b)
        {
            if (a is CircleShape aCircle)
            {
                if (b is CircleShape bCircle)
                    return Collides(aCircle, bCircle);
                else if (b is LineShape bLine)
                    return Collides(aCircle, bLine);
                else if (b is PolygonShape bPolygon)
                    return Collides(aCircle, bPolygon);
            } else if (a is LineShape aLine)
            {
                if (b is CircleShape bCircle)
                    return Collides(bCircle, aLine);
                else if (b is LineShape bLine)
                    return Collides(aLine, bLine);
                else if (b is PolygonShape bPolygon)
                    return Collides(aLine, bPolygon);
            } else if (a is PolygonShape aPolygon)
            {
                if (b is CircleShape bCircle)
                    return Collides(bCircle, aPolygon);
                else if (b is LineShape bLine)
                    return Collides(bLine, aPolygon);
                else if (b is PolygonShape bPolygon)
                    return Collides(aPolygon, bPolygon);
            } else if (a is CompoundShape aCompound)
                return Collides(aCompound, b);
            else if (b is CompoundShape bCompound)
                return Collides(bCompound, a);

            throw new NotImplementedException();
        }

        public static bool Collides(CompoundShape a, Shape b)
        {
            return a.Shapes.Any(s => s.CollidesWith(b));
        }

        public static bool Collides(CircleShape a, CircleShape b)
        {
            var distance = (b.Center - a.Center).Length();
            return a.Radius + b.Radius >= distance;
        }

        public static bool Collides(CircleShape a, LineShape b)
        {
            return CollidesCircleWithLine(a.Center, a.Radius, b.PointA, b.PointB);
        }

        public static bool Collides(CircleShape a, PolygonShape b)
        {
            for (var i = 0; i < b.Vertices.Count; ++i)
            {
                var nx = (i + 1) % b.Vertices.Count;
                if (CollidesCircleWithLine(a.Center, a.Radius, b.Vertices[i], b.Vertices[nx]))
                    return true;
            }

            return false;
        }

        public static bool Collides(LineShape a, LineShape b)
        {
            return CollidesSegmentWidthSegment(a.PointA, a.PointB, b.PointA, b.PointB);
        }

        public static bool Collides(LineShape a, PolygonShape b)
        {
            for (var i = 0; i < b.Vertices.Count; ++i)
            {
                var nx = (i + 1) % b.Vertices.Count;
                if (CollidesSegmentWidthSegment(a.PointA, a.PointB, b.Vertices[i], b.Vertices[nx]))
                    return true;
            }

            return false;
        }

        public static bool Collides(PolygonShape a, PolygonShape b)
        {
            for (var i = 0; i < a.Vertices.Count; ++i)
            for (var j = 0; j < b.Vertices.Count; ++j)
            {
                var nxi = (i + 1) % a.Vertices.Count;
                var nxj = (j + 1) % b.Vertices.Count;
                if (CollidesSegmentWidthSegment(a.Vertices[i], a.Vertices[nxi], b.Vertices[i], b.Vertices[nxj]))
                    return true;
            }

            return false;
        }

        private static bool CollidesSegmentWidthSegment(Vector a, Vector b, Vector c, Vector d)
        {
            var top = (c - a).Y + (b - a).X - (c - a).X * (b - a).Y;
            var down = (d - c).X * (b - a).Y - (d - c).Y * (b - a).X;
            if (MathHelper.Abs(down) < 1e-5)
                return false; // IT IS NOT TRUE, BUT FINE FOR NOW
            var q = top / down;
            var p = (((c - a) + (d - c) * q) / (b - a)).X;
            return q >= 0 && q <= 1 && p >= 0 && p <= 1;
        }

        private static bool CollidesCircleWithLine(Vector c, float r, Vector a, Vector b)
        {
            //some slow code
            var left = 0f;
            var right = 1f;
            var ab = b - a;
            for (var iterations = 0; iterations < 32; ++iterations)
            {
                var m1 = (left + left + right) / 3;
                var m2 = (left + right + right) / 3;
                var p1 = a + ab * m1;
                var p2 = a + ab * m2;
                if (Vector.DistanceSquared(c, p1) < Vector.DistanceSquared(c, p2))
                    right = m2;
                else
                    left = m1;
            }

            var p = a + ab * left;

            return Vector.DistanceSquared(c, p) <= r;
        }
    }
}