using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RPG.Core;
using RPG.Core.Animations;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.NodeVisitors.Input;
using RPG.NodeVisitors.Pointer;
using RPG.NodeVisitors.Update;
using RPG.SceneControls;
using RPG.SceneGraph;

namespace RPG.Runner
{
    public class SCHero : SceneControl<HeroAspect>
    {
        private readonly Node _root = new Node();
        private InputNode _inputNode;
        private PointerNode _pointerNode;

        private TransformNode _aimTransformNode;

        //private AspectAnimation<Matrix> _punchAnimation;
        //private AspectAnimation<Matrix> _shieldAnimation;

        public SCHero(HeroAspect aspect, ICreator creator) : base(aspect, creator)
        {
        }

        public override Node RootNode => _root;

        protected override void Initialize()
        {
            var handDrawable = new DrawableNode() { Drawable = new CircleDrawable(Aspect.HandRadius) };
            var handDirDrawable = new DrawableNode() {Drawable = new LineDrawable(Aspect.HandRadius * 2)};

            var leftHand = new SCTransform(Aspect.LeftHandTransform);
            leftHand.RootNode.AddChild(handDrawable);
            leftHand.RootNode.AddChild(handDirDrawable);

            var shield = new DrawableNode() { Drawable = new LineDrawable(36) { BorderPen = new Pen(Color.Red, 5) } };
            var shieldTransform = new TransformNode() { Transform = Matrix.CreateRotation(MathHelper.ToRadians(90)) * Matrix.CreateTranslate(Vector.UnitX * 8) };
            shieldTransform.AddChild(shield);
            leftHand.RootNode.AddChild(shieldTransform);

            var rightHand = new SCTransform(Aspect.RightHandTransform);
            rightHand.RootNode.AddChild(handDrawable);
            rightHand.RootNode.AddChild(handDirDrawable);


            var sword = new DrawableNode() { Drawable = new LineDrawable(48) { BorderPen = new Pen(Color.SaddleBrown, 3) } };
            var swordTransform = new TransformNode() { Transform = Matrix.CreateTranslate(new Vector(12, 0)) };
            swordTransform.AddChild(sword);
            rightHand.RootNode.AddChild(swordTransform);

            var bodyDrawable = new DrawableNode() {Drawable = new CircleDrawable(Aspect.BodyRadius)};
            var bodyTransform = new SCTransform(Aspect.Transform);
            bodyTransform.RootNode.AddChild(bodyDrawable);

            var leftArmTransform = new TransformNode() {Transform = Matrix.CreateTranslate(new Vector(20, -16))};
            leftArmTransform.AddChild(leftHand.RootNode);
            bodyTransform.RootNode.AddChild(leftArmTransform);


            var rightArmTransform = new TransformNode() { Transform = Matrix.CreateTranslate(new Vector(20, 16)) };
            rightArmTransform.AddChild(rightHand.RootNode);
            bodyTransform.RootNode.AddChild(rightArmTransform);

            _root.AddChild(bodyTransform.RootNode);

            _inputNode = new InputNode();
            _root.AddChild(_inputNode);

            var updateNode = new UpdateNode {UpdateAction = UpdateAction};
            _root.AddChild(updateNode);

            _pointerNode = new PointerNode();
            var aimDrawable = new DrawableNode { Drawable = new CircleDrawable(3) };
            _pointerNode.AddChild(aimDrawable);
            bodyTransform.RootNode.AddChild(_pointerNode);

            /*var punchMatrix = Matrix.CreateRotation(MathHelper.ToRadians(-45)) *
                               Matrix.CreateTranslate(new Vector(18, -6));
            _punchAnimation = new CompositeAnimation<Matrix>(new List<AspectAnimation<Matrix>>
            {
                new LinearAnimation(Matrix.Identity, punchMatrix, 0.05f),
                new LinearAnimation(punchMatrix, Matrix.Identity, 0.2f)
            });
            _punchAnimation.AddTarget(Aspect.RightHandTransform);

            var shieldMatrix = Matrix.CreateTranslate(new Vector(18, 0));
            _shieldAnimation = new CompositeAnimation<Matrix>(new List<AspectAnimation<Matrix>>
            {
                new LinearAnimation(Matrix.Identity, shieldMatrix, 0.05f),
                new LinearAnimation(shieldMatrix, Matrix.Identity, 0.2f)
            });
            _shieldAnimation.AddTarget(Aspect.LeftHandTransform);*/

        }

        private void UpdateAction(float deltaTime)
        {
            var inputState = _inputNode.InputState;
            if (inputState.IsKeyDown(Keys.D))
                Aspect.Move(0, 200 * deltaTime);
            if (inputState.IsKeyDown(Keys.A))
                Aspect.Move(0, -200 * deltaTime);
            if (inputState.IsKeyDown(Keys.S))
                Aspect.Move(-200 * deltaTime, 0);
            if (inputState.IsKeyDown(Keys.W))
                Aspect.Move(200 * deltaTime, 0);
            /*if (inputState.IsMouseButtonDown(MouseButtons.Left))
                Punch();
            if (inputState.IsMouseButtonDown(MouseButtons.Right))
                Block();*/

            var pointerLocation = _pointerNode.Transform.Translation;
            Aspect.SetRotation((float)System.Math.Atan2(pointerLocation.Y, pointerLocation.X));

            /*_punchAnimation.Update(deltaTime);
            _shieldAnimation.Update(deltaTime);*/
        }

        public override void Dispose()
        {
            
        }

        /*private void Punch()
        {
            if (_punchAnimation.IsOver || !_punchAnimation.Started)
            {
                _punchAnimation.Start();
            }
        }

        private void Block()
        {
            if (_shieldAnimation.IsOver || !_shieldAnimation.Started)
            {
                _shieldAnimation.Start();
            }
        }*/
    }
}