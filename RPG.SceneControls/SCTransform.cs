using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Core;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.SceneGraph;

namespace RPG.SceneControls
{
    public class SCTransform : SceneControl<Aspect<Matrix>>
    {
        private TransformNode _root;

        public SCTransform(Aspect<Matrix> aspect) : base(aspect, null)
        {
        }

        public override Node RootNode => _root;

        protected override void Initialize()
        {
            _root = new TransformNode() {Transform = Aspect.Value};
            Aspect.ValueChanged += Aspect_ValueChanged;
        }

        private void Aspect_ValueChanged()
        {
            _root.Transform = Aspect.Value;
        }

        public override void Dispose()
        {
            Aspect.ValueChanged -= Aspect_ValueChanged;
        }
    }
}
