namespace RPG.Math
{
    public abstract class Shape
    {
        private BoundingBox _boundingBox;
        private bool _boundingBoxNeedRecalc = true;

        public BoundingBox BoundingBox
        {
            get
            {
                if (_boundingBoxNeedRecalc)
                {
                    _boundingBox = CalculateBoundingBox();
                    _boundingBoxNeedRecalc = false;
                }

                return _boundingBox;
            }
        }

        protected abstract BoundingBox CalculateBoundingBox();

        public abstract void ApplyTransform(Matrix transform);

        public abstract Shape Clone();
    }
}