using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Tree : IAspect
    {
        public Matrix Transform { get; }

        public Tree(Matrix locationTransform)
        {
            Transform = locationTransform;
        }
    }
}