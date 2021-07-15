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
    public class SCStone : SceneControl<Stone>
    {
        public const float StoneRadius = 35;
        private TransformNode _root;

        public override Node RootNode => _root;

        public SCStone(Stone aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new CollisionNode(new CircleShape(Vector.Zero, StoneRadius)) { Transform = Aspect.Transform };

            var stone = new DrawableNode()
            {
                Drawable = new CircleDrawable(StoneRadius)
                    { BorderPen = new Pen(Color.Black, 4), FillBrush = new SolidBrush(Color.FromArgb(179, 179, 179)) }
            };
            _root.AddChild(stone);

            var flare = new DrawableNode()
            {
                Drawable = new CircleDrawable(StoneRadius / 4)
                    { BorderPen = Pens.Transparent, FillBrush = new SolidBrush(Color.FromArgb(202, 202, 204)) }
            };
            var flareTransform = new TransformNode(){Transform = Matrix.CreateTranslate(StoneRadius / 3, -StoneRadius / 3)};
            flareTransform.AddChild(flare);
            _root.AddChild(flareTransform);
        }

        public override void Dispose()
        {
            //
        }
    }
}