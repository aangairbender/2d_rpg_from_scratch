using RPG.Core;
using RPG.Math;

namespace RPG.Runner
{
    public class GameAspect : IAspect
    {
        public HeroAspect HeroAspect { get; } = new HeroAspect();
        public EnemyAspect EnemyAspect { get; } = new EnemyAspect();
    }
}