using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Bush : IAspect
    {
        public Matrix Transform { get; }

        public Bush(Matrix locationTransform)
        {
            Transform = locationTransform;
        }
    }
}