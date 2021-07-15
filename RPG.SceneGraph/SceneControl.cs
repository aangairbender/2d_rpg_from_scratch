using System;
using System.Collections.Generic;
using RPG.Core.Mapping;
using RPG.SceneGraph;

namespace RPG.Core
{
    public abstract class SceneControl<T> : ISceneControl, IDisposable
        where T : IAspect
    {
        protected T Aspect { get; }

        protected ICreator Creator { get; }

        protected SceneControl(T aspect, ICreator creator)
        {
            Aspect = aspect;
            Creator = creator;

            Initialize();
            InitializeParentsForTree(RootNode);
        }

        private void InitializeParentsForTree(Node node)
        {
            if (node.ParentControl != null)
                return;

            node.ParentControl = this;
            foreach (var child in node.Children)
            {
                if (!(child is Node childNode))
                    continue;
                InitializeParentsForTree(childNode);
            }
        }

        protected abstract void Initialize();

        public abstract Node RootNode { get; }

        public abstract void Dispose();
    }
}