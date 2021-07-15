using System.Drawing;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Draw.Drawables
{
    public class CircleDrawable : IDrawable
    {
        public float Radius { get; set; }
        public Brush FillBrush { get; set; } = Brushes.White;
        public Pen BorderPen { get; set; } = Pens.Black;

        public CircleDrawable(float radius)
        {
            Radius = radius;
        }

        public void Draw(Graphics g)
        {
            var rect = new RectangleF(-Radius, -Radius, Radius * 2, Radius * 2);
            g.FillEllipse(FillBrush, rect);
            g.DrawEllipse(BorderPen, rect);
        }
    }
}