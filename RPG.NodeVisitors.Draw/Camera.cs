using RPG.Math;

namespace RPG.NodeVisitors.Draw
{
    public class Camera
    {
        public float Width { get; }
        public float Height { get; }

        public Matrix Transform { get; private set; } = Matrix.Identity;

        public Camera(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public void Move(float dx, float dy)
        {
            Transform *= Matrix.CreateTranslate(dx, dy);
        }

        public void ZoomIn(float times)
        {

        }

        public void ZoomOut(float times)
        {

        }
    }
}