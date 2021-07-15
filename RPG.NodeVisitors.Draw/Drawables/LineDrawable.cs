using System.Drawing;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Draw.Drawables
{
    public class LineDrawable : IDrawable
    {
        public float Length { get; set; }

        public Pen BorderPen { get; set; } = Pens.Black;

        public LineDrawable(float length)
        {
            Length = length;
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(BorderPen, -Length / 2, 0, Length / 2, 0);
        }
    }
}