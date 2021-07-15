using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.Math.Shapes;
using RPG.NodeVisitors.Collisions;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.NodeVisitors.Input;
using RPG.NodeVisitors.Pointer;
using RPG.NodeVisitors.Update;
using RPG.Runner.Domain;
using RPG.SceneControls;
using RPG.SceneGraph;

namespace RPG.Runner.Controls
{
    public class SCCharacter : SceneControl<Character>
    {
        public const float BodyRadius = 24;
        public Matrix LeftArmTransform = Matrix.CreateTranslate(new Vector(20, -16));
        public Matrix RightArmTransform = Matrix.CreateTranslate(new Vector(20, -16)) * Matrix.CreateScale(1, -1);

        private readonly Node _root = new Node();
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private PointerNode _pointerNode;
        private InputNode _inputNode;
        private CollisionNode _bodyNode;

        private Matrix _moveTarget;
        private bool _isMoving;

        public override Node RootNode => _root;

        public SCCharacter(Character aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            var bodyDrawable = new DrawableNode()
                {Drawable = new CircleDrawable(BodyRadius) {FillBrush = new SolidBrush(Color.FromArgb(254, 236, 150))}};
            _bodyNode = new CollisionNode(new CircleShape(Vector.Zero, BodyRadius)) {IsTrigger = false, Transform = Aspect.Transform.Value};
            Aspect.Transform.ValueChanged += () => _bodyNode.Transform = Aspect.Transform.Value;
            _bodyNode.AddChild(bodyDrawable);
            _root.AddChild(_bodyNode);

            var leftArmTransform = new TransformNode() { Transform = LeftArmTransform};
            leftArmTransform.AddChild(Creator.CreateFor(Aspect.LeftHand).RootNode);
            _bodyNode.AddChild(leftArmTransform);

            var rightArmTransform = new TransformNode() { Transform = RightArmTransform };
            rightArmTransform.AddChild(Creator.CreateFor(Aspect.RightHand).RootNode);
            _bodyNode.AddChild(rightArmTransform);

            _pointerNode = new PointerNode();
            _bodyNode.AddChild(_pointerNode);

            _inputNode = new InputNode();
            _root.AddChild(_inputNode);

            var updateNode = new UpdateNode { UpdateAction = UpdateAction };
            _root.AddChild(updateNode);
        }

        private void UpdateAction(float deltaTime)
        {
            if (!_bodyNode.Transform.Equals(Aspect.Transform.Value))
                Aspect.Transform.Value = _bodyNode.Transform;
            UpdateMovement(deltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {
            if (_inputNode.InputState.IsMouseButtonDown(MouseButtons.Right))
            {
                _moveTarget = _pointerNode.Transform * Aspect.Transform.Value;
                _isMoving = true;
            }

            if (!_isMoving)
                return;

            var target = (_moveTarget * Aspect.Transform.Value.Inverted()).Translation;

            var distance = target.Length();

            if (distance < 1e-1)
            {
                _isMoving = false;
                return;
            }

            var angleOffset = MathHelper.Atan2(target.Y, target.X);

            var rotationOnly = (MathHelper.Abs(angleOffset) > 0.26f);

            var maxAngle = Aspect.RotationSpeed.Value * deltaTime;
            if (MathHelper.Abs(angleOffset) >= maxAngle)
                angleOffset *= maxAngle / MathHelper.Abs(angleOffset);

            Aspect.Rotate(angleOffset);

            if (rotationOnly)
                return;

            if (distance >= Aspect.MovementSpeed.Value * deltaTime)
                distance = Aspect.MovementSpeed.Value * deltaTime;

            Aspect.MoveForward(distance);
        }

        public override void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}