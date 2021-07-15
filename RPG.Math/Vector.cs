using System;
using System.Runtime.CompilerServices;

namespace RPG.Math
{
    public struct Vector : IEquatable<Vector>
    {
        public float X;
        public float Y;

        public static Vector Zero { get; } = new Vector(0, 0);
        public static Vector One { get; } = new Vector(1, 1);
        public static Vector UnitX { get; } = new Vector(1, 0);
        public static Vector UnitY { get; } = new Vector(0, 1);

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length()
        {
            return (float)System.Math.Sqrt((X * X) + (Y * Y));
        }

        public float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public void Normalize()
        {
            var val = 1.0f / (float)System.Math.Sqrt((X * X) + (Y * Y));
            X *= val;
            Y *= val;
        }

        public Vector Normalized()
        {
            var val = 1.0f / (float)System.Math.Sqrt((X * X) + (Y * Y));
            return new Vector(X * val, Y * val);
        }

        public Vector Transformed(Matrix transform)
        {
            return new Vector(
                X * transform.M11 + Y * transform.M12 + transform.M31,
                X * transform.M21 + Y * transform.M22 + transform.M32);
        }

        public bool Equals(Vector other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }

        public static Vector operator -(Vector value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static Vector operator +(Vector value1, Vector value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        public static Vector operator -(Vector value1, Vector value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static Vector operator *(Vector value, float factor)
        {
            value.X *= factor;
            value.Y *= factor;
            return value;
        }

        public static Vector operator *(float factor, Vector value)
        {
            value.X *= factor;
            value.Y *= factor;
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator /(Vector value1, Vector value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public void SetLength(float length)
        {
            var curLength = Length();
            X *= length / curLength;
            Y *= length / curLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator /(Vector value1, float divider)
        {
            var factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        public static bool operator ==(Vector value1, Vector value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }

        public static bool operator !=(Vector value1, Vector value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }

        public static float Distance(ref Vector value1, ref Vector value2)
        {
            var v1 = value1.X - value2.X;
            var v2 = value1.Y - value2.Y;
            return (float)System.Math.Sqrt(v1 * v1 + v2 * v2);
        }

        public static float DistanceSquared(Vector value1, Vector value2)
        {
            var v1 = value1.X - value2.X;
            var v2 = value1.Y - value2.Y;
            return (v1 * v1) + (v2 * v2);
        }

        public static float Dot(Vector value1, Vector value2)
        {
            return (value1.X * value2.X) + (value1.Y * value2.Y);
        }
    }
}
