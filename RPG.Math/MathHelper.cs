namespace RPG.Math
{
    public static class MathHelper
    {
        public const float Pi = (float)System.Math.PI;

        public const float PiOver2 = (float)(System.Math.PI / 2.0);
        
        public const float PiOver4 = (float)(System.Math.PI / 4.0);
        
        public const float TwoPi = (float)(System.Math.PI * 2.0);

        public static void Swap<T>(ref T a, ref T b)
        {
            var c = a;
            a = b;
            b = c;
        }

        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        public static int Max(int value1, int value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }
        
        public static int Min(int value1, int value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        public static float ToDegrees(float radians)
        {
            return (float)(radians * 57.295779513082320876798154814105);
        }

        public static float ToRadians(float degrees)
        {
            return (float)(degrees * 0.017453292519943295769236907684886);
        }

        public static float WrapAngle(float angle)
        {
            if ((angle > -Pi) && (angle <= Pi))
                return angle;
            angle %= TwoPi;
            if (angle <= -Pi)
                return angle + TwoPi;
            if (angle > Pi)
                return angle - TwoPi;
            return angle;
        }

        public static float Abs(float value)
        {
            if (value > 0)
                return value;
            return -value;
        }

        public static float Atan2(float valueY, float valueX)
        {
            return WrapAngle((float)System.Math.Atan2(valueY, valueX));
        }

        public static float Sqrt(float val)
        {
            return (float) System.Math.Sqrt(val);
        }
    }
}