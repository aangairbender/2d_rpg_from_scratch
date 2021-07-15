using System;
using System.Collections.Generic;
using RPG.Core;
using RPG.Core.Animations;
using RPG.Math;

namespace RPG.Runner
{
    public class HeroAspect : IAspect
    {
        public float BodyRadius { get; } = 24;
        public float HandRadius { get;  } = 6;


        public Aspect<Matrix> Transform { get; set; } = new Aspect<Matrix>(Matrix.CreateTranslate(new Vector(50, 100)));

        public Aspect<Matrix> LeftHandTransform { get; set; } = new Aspect<Matrix>(Matrix.Identity);

        public Aspect<Matrix> RightHandTransform { get; set; } = new Aspect<Matrix>(Matrix.Identity);

        public void Move(float offsetX, float offsetY)
        {
            var rotation = Transform.Value.Rotation;
            Transform.Value *= Matrix.CreateRotation(-rotation);
            Transform.Value *= Matrix.CreateTranslate(new Vector(offsetX, offsetY));
            Transform.Value *= Matrix.CreateRotation(rotation);
        }

        public void SetRotation(float rotation)
        {
            var translateVector = Transform.Value.Translation;
            Transform.Value *= Matrix.CreateTranslate(-translateVector);
            Transform.Value *= Matrix.CreateRotation(rotation);
            Transform.Value *= Matrix.CreateTranslate(translateVector);
        }
    }
}