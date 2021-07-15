using System.Collections.Generic;
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
    public class SCBuilding : SceneControl<Building>
    {
        public const float Width = 350;
        public const float Height = 350;
        public const float WallWidth = 10;
        public const float Doorstep = 100;

        private TransformNode _root;
        private SwitchNode _ceilingSwitch;

        public override Node RootNode => _root;

        public SCBuilding(Building aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new TransformNode() {Transform = Aspect.Transform};

            var ceiling = new DrawableNode()
            {
                Drawable = new RectangleDrawable(Width, Height)
                {
                    FillBrush = Brushes.DarkSalmon
                }
            };
            _ceilingSwitch = new SwitchNode();
            _ceilingSwitch.AddChild(ceiling);

            var doorStep = new DrawableNode()
            {
                Drawable = new RectangleDrawable(Width + Doorstep * 2, Height + Doorstep * 2)
                {
                    FillBrush = Brushes.DarkGray,
                    BorderPen = Pens.Black
                }
            };
            _root.AddChild(doorStep);

            var doorStepTrigger = new CollisionNode(new PolygonShape(new List<Vector>
            {
                new Vector(-Width / 2, -Height / 2),
                new Vector(-Width / 2, Height / 2),
                new Vector(Width / 2, Height / 2),
                new Vector(Width / 2, -Height / 2),
            })) {IsTrigger = true};
            doorStepTrigger.TriggerEnter += sc =>
            {
                if (sc.GetType() != typeof(SCCharacter))
                    return;
                _ceilingSwitch.Enabled = false;
            };
            doorStepTrigger.TriggerLeave += sc =>
            {
                if (sc.GetType() != typeof(SCCharacter))
                    return;
                _ceilingSwitch.Enabled = true;
            };
            _root.AddChild(doorStepTrigger);

            var floor = new DrawableNode()
            {
                Drawable = new RectangleDrawable(Width, Height)
                {
                    FillBrush = Brushes.SaddleBrown
                }
            };
            _root.AddChild(floor);

            var wallHorizontal = new DrawableNode
            {
                Drawable = new RectangleDrawable(Width, WallWidth)
                {
                    FillBrush = Brushes.Brown,
                    BorderPen = Pens.Black
                }
            };
            var wallVertical = new DrawableNode
            {
                Drawable = new RectangleDrawable(WallWidth, Height)
                {
                    FillBrush = Brushes.Brown,
                    BorderPen = Pens.Black
                }
            };

            var topWall = new TransformNode() {Transform = Matrix.CreateTranslate(0, Height / 2)};
            topWall.AddChild(wallHorizontal);
            var bottomWall = new TransformNode() { Transform = Matrix.CreateTranslate(0, -Height / 2) };
            bottomWall.AddChild(wallHorizontal);
            var leftWall = new TransformNode() { Transform = Matrix.CreateTranslate(Width / 2, 0) };
            leftWall.AddChild(wallVertical);
            var rightWall = new TransformNode() { Transform = Matrix.CreateTranslate(Width / 2, 0) };
            rightWall.AddChild(wallVertical);

            _root.AddChild(topWall);
            _root.AddChild(bottomWall);
            _root.AddChild(leftWall);
            _root.AddChild(rightWall);

            _root.AddChild(_ceilingSwitch);
        }

        public override void Dispose()
        {
            //
        }
    }
}