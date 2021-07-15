using RPG.Core;
using RPG.Core.Mapping;
using RPG.SceneGraph;

namespace RPG.SceneControls
{
    public class SCSwitch : SceneControl<Aspect<bool>>
    {
        private SwitchNode _root;

        public SCSwitch(Aspect<bool> aspect) : base(aspect, null)
        {
        }

        public override Node RootNode => _root;

        protected override void Initialize()
        {
            _root = new SwitchNode() {Enabled = Aspect.Value};
            Aspect.ValueChanged += Aspect_ValueChanged;
        }

        private void Aspect_ValueChanged()
        {
            _root.Enabled = Aspect.Value;
        }

        public override void Dispose()
        {
            Aspect.ValueChanged -= Aspect_ValueChanged;
        }
    }
}