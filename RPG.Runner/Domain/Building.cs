using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Building : IAspect
    {
        public Matrix Transform { get; }

        public Building(Matrix locationTransform)
        {
            Transform = locationTransform;
        }
    }
}