using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Hand : IAspect
    {
        public Aspect<Matrix> Transform { get; set; } = new Aspect<Matrix>(Matrix.Identity);
    }
}