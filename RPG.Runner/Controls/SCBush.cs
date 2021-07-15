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
    public class SCBush : SceneControl<Bush>
    {
        public const float BushRadius = 40;

        private CollisionNode _root;

        public override Node RootNode => _root;

        public SCBush(Bush aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new CollisionNode(new CircleShape(Vector.Zero, BushRadius)) { Transform = Aspect.Transform, IsTrigger = true};

            var layer1 = new DrawableNode()
            {
                Drawable = new CircleDrawable(BushRadius)
                    { BorderPen = Pens.Transparent, FillBrush = new SolidBrush(Color.FromArgb(230, 34, 74, 14)) }
            };
            _root.AddChild(layer1);

            var layer2 = new DrawableNode()
            {
                Drawable = new CircleDrawable(BushRadius * 2 / 3)
                    { BorderPen = Pens.Transparent, FillBrush = new SolidBrush(Color.FromArgb(230, 54, 124, 25)) }
            };
            _root.AddChild(layer2);

            var layer3 = new DrawableNode()
            {
                Drawable = new CircleDrawable(BushRadius / 3)
                    { BorderPen = Pens.Transparent, FillBrush = new SolidBrush(Color.FromArgb(230, 166, 217, 90)) }
            };
            _root.AddChild(layer3);

            _root.TriggerEnter += sc =>
            {
                if (sc.GetType() != typeof(SCCharacter))
                    return;
                (layer1.Drawable as CircleDrawable).BorderPen = Pens.Red;
            };
            _root.TriggerLeave += sc =>
            {
                if (sc.GetType() != typeof(SCCharacter))
                    return;
                (layer1.Drawable as CircleDrawable).BorderPen = Pens.Transparent;
            };
        }

        public override void Dispose()
        {
            //
        }
    }
}