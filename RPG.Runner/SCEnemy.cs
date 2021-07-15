using System.Drawing;
using System.Windows.Forms;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.NodeVisitors.Input;
using RPG.NodeVisitors.Update;
using RPG.SceneControls;
using RPG.SceneGraph;

namespace RPG.Runner
{
    public class SCEnemy : SceneControl<EnemyAspect>
    {
        private readonly Node _root = new Node();

        private InputNode _inputNode;

        public SCEnemy(EnemyAspect aspect, ICreator creator) : base(aspect, creator)
        {
        }

        public override Node RootNode => _root;

        protected override void Initialize()
        {
            var bodyNode = new SCTransform(Aspect.Transform);

            var bodyDrawable = new DrawableNode
                {Drawable = new CircleDrawable(20) {BorderPen = new Pen(Color.Black, 2), FillBrush = Brushes.Red}};
            bodyNode.RootNode.AddChild(bodyDrawable);

            var headDrawable = new DrawableNode
                {Drawable = new CircleDrawable(12) {BorderPen = new Pen(Color.Black, 2), FillBrush = Brushes.Orange}};
            bodyNode.RootNode.AddChild(headDrawable);

            _root.AddChild(bodyNode.RootNode);

            var updateNode = new UpdateNode();
            updateNode.UpdateAction = UpdateAction;
            _root.AddChild(updateNode);

            _inputNode = new InputNode();
            _root.AddChild(_inputNode);
        }

        private void UpdateAction(float deltaTime)
        {
            var input = _inputNode.InputState;
            if (input.IsKeyDown(Keys.NumPad6))
                Aspect.Move(100 * deltaTime, 0);
            if (input.IsKeyDown(Keys.NumPad4))
                Aspect.Move(-100 * deltaTime, 0);
            if (input.IsKeyDown(Keys.NumPad8))
                Aspect.Move(0, -100 * deltaTime);
            if (input.IsKeyDown(Keys.NumPad5))
                Aspect.Move(0, 100 * deltaTime);
        }

        public override void Dispose()
        {
            
        }
    }
}