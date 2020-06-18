using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Controllers;
using TowerDefence.Views;

namespace TowerDefence.Screens
{
    public abstract class BaseScreen
    {
        protected List<IController> controllers;
        protected List<IView> views;

        public BaseScreen()
        {
            controllers = new List<IController>();
            views = new List<IView>();
        }

        public virtual void Update(float deltaTime)
        {
            foreach (IController controller in controllers)
                if (controller.Enabled) controller.Update(deltaTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
