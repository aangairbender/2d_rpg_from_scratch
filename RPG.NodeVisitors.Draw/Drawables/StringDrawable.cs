using System.Drawing;

namespace RPG.NodeVisitors.Draw.Drawables
{
    public class StringDrawable : IDrawable
    {
        public string Content { get; set; }

        public Font Font { get; set; } = new Font("Arial", 16);

        public Brush FillBrush { get; set; } = Brushes.Yellow;

        public StringDrawable(string content)
        {
            Content = content;
        }

        public void Draw(Graphics g)
        {
            var size = g.MeasureString(Content, Font);
            g.DrawString(Content, Font, FillBrush, -size.Width / 2, -size.Height / 2);
        }
    }
}