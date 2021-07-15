using System.Drawing;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.NodeVisitors.Draw;
using RPG.NodeVisitors.Draw.Drawables;
using RPG.NodeVisitors.Pointer;
using RPG.NodeVisitors.Update;
using RPG.Runner.Domain;
using RPG.SceneGraph;

namespace RPG.Runner.Controls
{
    public class SCLocation : SceneControl<Location>
    {
        private Node _root;

        private PointerNode _pointerNode;
        private CameraNode _cameraNode;

        public override Node RootNode => _root;

        public SCLocation(Location aspect, ICreator creator) : base(aspect, creator)
        {
        }

        protected override void Initialize()
        {
            _root = new Node();
            _root.AddChild(new DrawableNode()
            {
                Drawable = new RectangleDrawable(Aspect.Bounds.Width, Aspect.Bounds.Height)
                    {FillBrush = Brushes.LimeGreen}
            });


            foreach (var building in Aspect.Buildings)
            {
                _root.AddChild(Creator.CreateFor(building).RootNode);
            }
            _root.AddChild(Creator.CreateFor(Aspect.Character).RootNode);

            foreach (var stone in Aspect.Stones)
            {
                _root.AddChild(Creator.CreateFor(stone).RootNode);
            }

            foreach (var bush in Aspect.Bushes)
            {
                _root.AddChild(Creator.CreateFor(bush).RootNode);
            }

            foreach (var tree in Aspect.Trees)
            {
                _root.AddChild(Creator.CreateFor(tree).RootNode);
            }



            _pointerNode = new PointerNode();
            var aimDrawable = new DrawableNode { Drawable = new CircleDrawable(3){FillBrush = Brushes.Red} };
            _pointerNode.AddChild(aimDrawable);
            _root.AddChild(_pointerNode);

            _cameraNode = new CameraNode();
            _root.AddChild(_cameraNode);

            var updateNode = new UpdateNode() {UpdateAction = UpdateAction};
            _root.AddChild(updateNode);
        }

        private void UpdateAction(float deltaTime)
        {
            var camera = _cameraNode.Camera;
            var pointer = (camera.Transform.Inverted() * _pointerNode.Transform).Translation;

            var speedFactor = 0.8f * deltaTime;
            if (pointer.X <= 0.5)
                camera.Move(-camera.Width * speedFactor, 0);
            if (pointer.Y <= 0.5)
                camera.Move(0, -camera.Height * speedFactor);
            if (pointer.X >= camera.Width - 1.5)
                camera.Move(camera.Width * speedFactor, 0);
            if (pointer.Y >= camera.Height - 1.5)
                camera.Move(0, camera.Height * speedFactor);
        }

        public override void Dispose()
        {
            //
        }
    }
}