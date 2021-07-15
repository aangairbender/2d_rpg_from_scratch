using RPG.Core;
using RPG.Math;

namespace RPG.Runner
{
    public class EnemyAspect : IAspect
    {
        public Aspect<Matrix> Transform { get; set; } = new Aspect<Matrix>(Matrix.CreateTranslate(new Vector(300, 100)));

        public void Move(float offsetX, float offsetY)
        {
            var rotation = Transform.Value.Rotation;
            Transform.Value *= Matrix.CreateRotation(-rotation);
            Transform.Value *= Matrix.CreateTranslate(new Vector(offsetX, offsetY));
            Transform.Value *= Matrix.CreateRotation(rotation);
        }

    }
}