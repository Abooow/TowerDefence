using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Controllers;
using TowerDefence.Moldels;
using TowerDefence.Views;

namespace TowerDefence.Managers
{
    public class ButtonManager : IController, IView
    {
        public HashSet<int> ControllerGroupId { get; }
        public HashSet<int> ViewGroupId { get; }
        public bool Enabled { get; set; }

        private List<Button> buttons;
        private MouseState lastMouse;

        public ButtonManager()
        {
            buttons = new List<Button>();
            lastMouse = Mouse.GetState();

            ControllerGroupId = new HashSet<int>();
            ViewGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void AddButton(Button button)
        {
            if (button != null && !buttons.Contains(button))
            {
                buttons.Add(button);
            }
        }

        public void AddButtons(ICollection<Button> buttons)
        {
            foreach (Button button in buttons)
            {
                if (button != null && !this.buttons.Contains(button))
                {
                    this.buttons.Add(button);
                }
            }
        }

        public void Update(float deltaTime)
        {
            MouseState mouseState = Mouse.GetState();

            foreach (Button button in buttons)
            {
                button.Update(mouseState, mouseState.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released);
            }

            lastMouse = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
