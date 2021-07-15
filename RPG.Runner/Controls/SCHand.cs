using System.Drawing;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.Runner.Domain;
using RPG.SceneControls;
using RPG.SceneGraph;

namespace RPG.Runner.Controls
{
    public class SCHand : SceneControl<Hand>
    {
        public const float HandRadius = 6;

        private SCTransform _root;

        public override Node RootNode => _root.RootNode;

        public SCHand(Hand aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new SCTransform(Aspect.Transform);

            var handDrawable = new DrawableNode()
                {Drawable = new CircleDrawable(HandRadius) {FillBrush = new SolidBrush(Color.FromArgb(233, 158, 7))}};
            var handDirDrawable = new DrawableNode() { Drawable = new LineDrawable(HandRadius * 2) };
            _root.RootNode.AddChild(handDrawable);
            _root.RootNode.AddChild(handDirDrawable);
        }

        public override void Dispose()
        {
            _root.Dispose();
        }
    }
}