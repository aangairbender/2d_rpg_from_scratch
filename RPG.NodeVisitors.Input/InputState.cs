using System.Collections.Generic;
using System.Windows.Forms;

namespace RPG.SceneGraph
{
    public class InputState
    {
        protected readonly Dictionary<Keys, bool> Keys = new Dictionary<Keys, bool>();
        protected readonly Dictionary<MouseButtons, bool> MouseButtons = new Dictionary<MouseButtons, bool>();

        public bool IsKeyDown(Keys key) => Keys.ContainsKey(key) && Keys[key];

        public bool IsMouseButtonDown(MouseButtons mouseButton) => MouseButtons.ContainsKey(mouseButton) && MouseButtons[mouseButton];
    }
}