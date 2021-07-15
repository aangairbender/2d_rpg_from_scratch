using RPG.Math;

namespace RPG.Core.Animations
{
    public class MatrixLinearAnimation : AnimationBase<Matrix>
    {
        private readonly float _rotationDelta;
        private readonly Vector _translateVector;

        public Matrix From { get; }
        public Matrix To { get; }

        public MatrixLinearAnimation(Matrix from, Matrix to)
        {
            From = from;
            To = to;

            var delta = to * from.Inverted();
            _rotationDelta = delta.Rotation;
            _translateVector = delta.Translation;
        }

        public override Matrix CalculateValueFor(float time)
        {
            return From * (Matrix.CreateRotation(_rotationDelta * time) * Matrix.CreateTranslate(_translateVector * time));
        }
    }
}