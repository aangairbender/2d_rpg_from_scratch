using System.Drawing;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.Math.Shapes;
using RPG.NodeVisitors.Collisions;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.Runner.Domain;
using RPG.SceneGraph;

namespace RPG.Runner.Controls
{
    public class SCTree : SceneControl<Tree>
    {
        public const float TrunkRadius = 30;
        public const float LeavesRadius = TrunkRadius * 3;

        private TransformNode _root;

        public override Node RootNode => _root;
        public SCTree(Tree aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new TransformNode(){Transform = Aspect.Transform};

            var leavesDrawable = new DrawableNode()
            {
                Drawable = new CircleDrawable(LeavesRadius)
                    {BorderPen = Pens.Transparent, FillBrush = new SolidBrush(Color.FromArgb(200, 57, 104, 32))}
            };
            _root.AddChild(leavesDrawable);

            var trunkDrawable = new DrawableNode()
            {
                Drawable = new CircleDrawable(TrunkRadius)
                    {BorderPen = new Pen(Color.FromArgb(29, 37, 24), 4), FillBrush = new SolidBrush(Color.FromArgb(69, 53, 17))}
            };
            var trunkCollision = new CollisionNode(new CircleShape(Vector.Zero, TrunkRadius));
            trunkCollision.AddChild(trunkDrawable);
            _root.AddChild(trunkCollision);
        }

        public override void Dispose()
        {
            //
        }
    }
}