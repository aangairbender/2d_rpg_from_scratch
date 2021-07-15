using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Character : IAspect
    {
        public Aspect<Matrix> Transform { get; set; } = new Aspect<Matrix>(Matrix.Identity);

        public Hand LeftHand { get; } = new Hand();

        public Hand RightHand { get; } = new Hand();

        public Aspect<float> MovementSpeed { get; } = new Aspect<float>(200);

        public Aspect<float> RotationSpeed { get; } = new Aspect<float>(MathHelper.TwoPi * 2);

        public void Rotate(float angle)
        {
            var translateVector = Transform.Value.Translation;
            Transform.Value *= Matrix.CreateTranslate(-translateVector);
            Transform.Value *= Matrix.CreateRotation(angle);
            Transform.Value *= Matrix.CreateTranslate(translateVector);
        }

        public void MoveForward(float distance)
        {
            var rotation = Transform.Value.Rotation;
            Transform.Value *= Matrix.CreateRotation(-rotation);
            Transform.Value *= Matrix.CreateTranslate(new Vector(distance, 0));
            Transform.Value *= Matrix.CreateRotation(rotation);
        }
    }
}