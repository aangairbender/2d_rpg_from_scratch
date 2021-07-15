using System;
using System.Xml.Schema;

namespace RPG.Math
{
    public struct Matrix : IEquatable<Matrix>
    {
        public float M11;
        public float M12;
        public float M13;
        public float M21;
        public float M22;
        public float M23;
        public float M31;
        public float M32;
        public float M33;

        public static Matrix Identity { get; } =
            new Matrix(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1);

        public Vector Translation => new Vector(M31, M32);
        public float Rotation => MathHelper.Atan2(M12, M11);
        public float Scale => MathHelper.Sqrt(1 - M12 * M12) / M11;

        public Matrix(
            float m11, float m12, float m13,
            float m21, float m22, float m23,
            float m31, float m32, float m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        public Matrix Clone()
        {
            return new Matrix(
                M11, M12, M13,
                M21, M22, M23,
                M31, M32, M33);
        }

        public void Transpose()
        {
            MathHelper.Swap(ref M12, ref M21);
            MathHelper.Swap(ref M13, ref M31);
            MathHelper.Swap(ref M23, ref M32);
        }

        public Matrix Transposed()
        {
            var matrix = this.Clone();
            matrix.Transpose();
            return matrix;
        }

        public Matrix Inverted()
        {
            var res = new Matrix();

            // calculating matrix of minors
            res.M11 = M22 * M33 - M23 * M32;
            res.M12 = M21 * M33 - M23 * M31;
            res.M13 = M21 * M32 - M22 * M31;
            res.M21 = M12 * M33 - M13 * M32;
            res.M22 = M11 * M33 - M13 * M31;
            res.M23 = M11 * M32 - M12 * M31;
            res.M31 = M12 * M23 - M13 * M22;
            res.M32 = M11 * M23 - M13 * M21;
            res.M33 = M11 * M22 - M12 * M21;

            // matrix of co-factors
            res.M12 = -res.M12;
            res.M21 = -res.M21;
            res.M23 = -res.M23;
            res.M32 = -res.M32;

            // calculating determinate of original matrix
            var det = M11 * res.M11 + M12 * res.M12 + M13 * res.M13;

            // transpose
            res.Transpose();

            return res * (1 / det);
        }

        public static Matrix CreateRotation(float angle)
        {
            var cs = (float)System.Math.Cos(angle);
            var sn = (float)System.Math.Sin(angle);

            var matrix = Matrix.Identity;
            matrix.M11 = cs;
            matrix.M12 = sn;
            matrix.M21 = -sn;
            matrix.M22 = cs;
            return matrix;
        }

        public static Matrix CreateScale(float xFactor, float yFactor)
        {
            var matrix = Matrix.Identity;
            matrix.M11 = xFactor;
            matrix.M22 = yFactor;
            return matrix;
        }

        public static Matrix CreateTranslate(Vector offset)
        {
            return CreateTranslate(offset.X, offset.Y);
        }

        public static Matrix CreateTranslate(float offsetX, float offsetY)
        {
            var matrix = Matrix.Identity;
            matrix.M31 = offsetX;
            matrix.M32 = offsetY;
            return matrix;
        }

        public static Matrix operator *(Matrix valueA, Matrix valueB)
        {
            return new Matrix(
                valueA.M11 * valueB.M11 + valueA.M12 * valueB.M21 + valueA.M13 * valueB.M31,
                valueA.M11 * valueB.M12 + valueA.M12 * valueB.M22 + valueA.M13 * valueB.M32,
                valueA.M11 * valueB.M13 + valueA.M12 * valueB.M23 + valueA.M13 * valueB.M33,
                valueA.M21 * valueB.M11 + valueA.M22 * valueB.M21 + valueA.M23 * valueB.M31,
                valueA.M21 * valueB.M12 + valueA.M22 * valueB.M22 + valueA.M23 * valueB.M32,
                valueA.M21 * valueB.M13 + valueA.M22 * valueB.M23 + valueA.M23 * valueB.M33,
                valueA.M31 * valueB.M11 + valueA.M32 * valueB.M21 + valueA.M33 * valueB.M31,
                valueA.M31 * valueB.M12 + valueA.M32 * valueB.M22 + valueA.M33 * valueB.M32,
                valueA.M31 * valueB.M13 + valueA.M32 * valueB.M23 + valueA.M33 * valueB.M33);
        }

        public static Matrix operator *(Matrix value, float factor)
        {
            value.M11 *= factor;
            value.M12 *= factor;
            value.M13 *= factor;
            value.M21 *= factor;
            value.M22 *= factor;
            value.M23 *= factor;
            value.M31 *= factor;
            value.M32 *= factor;
            value.M33 *= factor;
            return value;
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return (
                matrix1.M11 == matrix2.M11 &&
                matrix1.M12 == matrix2.M12 &&
                matrix1.M13 == matrix2.M13 &&
                matrix1.M21 == matrix2.M21 &&
                matrix1.M22 == matrix2.M22 &&
                matrix1.M23 == matrix2.M23 &&
                matrix1.M31 == matrix2.M31 &&
                matrix1.M32 == matrix2.M32 &&
                matrix1.M33 == matrix2.M33
            );
        }

        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return (
                matrix1.M11 != matrix2.M11 ||
                matrix1.M12 != matrix2.M12 ||
                matrix1.M13 != matrix2.M13 ||
                matrix1.M21 != matrix2.M21 ||
                matrix1.M22 != matrix2.M22 ||
                matrix1.M23 != matrix2.M23 ||
                matrix1.M31 != matrix2.M31 ||
                matrix1.M32 != matrix2.M32 ||
                matrix1.M33 != matrix2.M33
            );
        }

        public bool Equals(Matrix other)
        {
            return (
                M11 == other.M11 &&
                M12 == other.M12 &&
                M13 == other.M13 &&
                M21 == other.M21 &&
                M22 == other.M22 &&
                M23 == other.M23 &&
                M31 == other.M31 &&
                M32 == other.M32 &&
                M33 == other.M33
            );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Matrix other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = M11.GetHashCode();
                hashCode = (hashCode * 397) ^ M12.GetHashCode();
                hashCode = (hashCode * 397) ^ M13.GetHashCode();
                hashCode = (hashCode * 397) ^ M21.GetHashCode();
                hashCode = (hashCode * 397) ^ M22.GetHashCode();
                hashCode = (hashCode * 397) ^ M23.GetHashCode();
                hashCode = (hashCode * 397) ^ M31.GetHashCode();
                hashCode = (hashCode * 397) ^ M32.GetHashCode();
                hashCode = (hashCode * 397) ^ M33.GetHashCode();
                return hashCode;
            }
        }
    }
}