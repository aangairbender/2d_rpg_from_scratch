using System.Drawing;

namespace RPG.NodeVisitors.Draw
{
    public interface IDrawable
    {
        void Draw(Graphics g);
    }
}