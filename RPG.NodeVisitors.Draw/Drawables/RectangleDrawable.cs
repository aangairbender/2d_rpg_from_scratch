using System.Drawing;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Draw.Drawables
{
    public class RectangleDrawable : IDrawable
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public Brush FillBrush { get; set; } = Brushes.White;
        public Pen BorderPen { get; set; } = Pens.Black;

        public RectangleDrawable(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(FillBrush, -Width / 2, -Height / 2, Width, Height);
            g.DrawRectangle(BorderPen, -Width / 2, -Height / 2, Width, Height);
        }
    }
}