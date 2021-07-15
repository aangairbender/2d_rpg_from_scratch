using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Stone : IAspect
    {
        public Matrix Transform { get; }

        public Stone(Matrix locationTransform)
        {
            Transform = locationTransform;
        }
    }
}