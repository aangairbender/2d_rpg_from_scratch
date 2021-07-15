using System.Windows.Forms;
using RPG.SceneGraph;

namespace RPG.NodeVisitors.Input
{
    public class MutableInputState : InputState
    {
        public void SetKeyDown(Keys key, bool isDown)
        {
            if (!Keys.ContainsKey(key))
                Keys.Add(key, isDown);
            else
                Keys[key] = isDown;
        }

        public void SetMouseButtonDown(MouseButtons mouseButton, bool isDown)
        {
            if (!MouseButtons.ContainsKey(mouseButton))
                MouseButtons.Add(mouseButton, isDown);
            else
                MouseButtons[mouseButton] = isDown;
        }
    }
}