using RPG.Core;
using RPG.Core.Mapping;
using RPG.Runner.Domain;
using RPG.SceneGraph;

namespace RPG.Runner.Controls
{
    public class SCWorld : SceneControl<World>
    {
        private Node _root;

        public override Node RootNode => _root;
        public SCWorld(World aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new Node();
            _root.AddChild(Creator.CreateFor(Aspect.Location).RootNode);
        }

        public override void Dispose()
        {
            //
        }
    }
}