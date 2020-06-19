using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MapEditor.Controllers;
using MapEditor.Views;

namespace MapEditor.Screens
{
    public abstract class BaseScreen
    {
        protected List<IController> controllers;
        protected List<IView> views;
        protected List<IUIView> uiViews;

        public BaseScreen()
        {
            controllers = new List<IController>();
            views = new List<IView>();
            uiViews = new List<IUIView>();
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
