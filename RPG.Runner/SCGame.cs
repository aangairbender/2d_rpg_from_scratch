using System.Drawing;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.NodeVisitors.Draw;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.NodeVisitors.Update;
using RPG.SceneControls;
using RPG.SceneGraph;

namespace RPG.Runner
{
    public class SCGame : SceneControl<GameAspect>
    {
        private readonly Node _root = new Node();

        public SCGame(GameAspect aspect, ICreator creator) : base(aspect, creator)
        {
        }

        public override Node RootNode => _root;

        protected override void Initialize()
        {
            _root.AddChild(Creator.CreateFor(Aspect.HeroAspect).RootNode);
            _root.AddChild(Creator.CreateFor(Aspect.EnemyAspect).RootNode);
        }

        public override void Dispose()
        {
            
        }
    }
}