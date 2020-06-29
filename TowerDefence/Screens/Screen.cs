using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Controllers;
using TowerDefence.Helpers;
using TowerDefence.Views;
using TowerDefence.Views.UI;

namespace TowerDefence.Screens
{
    public abstract class Screen
    {
        public HashSet<int> DisabledControllerGroupId { get; }
        public HashSet<int> DisabledViewGroupId { get; }

        protected List<IController> controllers;
        protected List<IUiView> uiViews;
        protected List<IView> views;

        public Screen()
        {
            DisabledControllerGroupId = new HashSet<int>();
            DisabledViewGroupId = new HashSet<int>();

            controllers = new List<IController>();
            uiViews = new List<IUiView>();
            views = new List<IView>();
        }

        public virtual void Update(float deltaTime)
        {
            foreach (IController controller in controllers)
            {
                if (controller.ControllerGroupId.ContainsAny(DisabledControllerGroupId)) continue;
                if (controller.Enabled) controller.Update(deltaTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public void DrawViews(SpriteBatch spriteBatch, SamplerState samplerState, Matrix camera, params IView[] extra)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, samplerState, null, null, null, camera);

            // Views.
            foreach (IView view in views)
                if (view.Enabled) view.Draw(spriteBatch);

            // Extra views.
            foreach (IView view in extra)
            {
                if (view.ViewGroupId.ContainsAny(DisabledViewGroupId)) continue;
                if (view.Enabled) view.Draw(spriteBatch);
            }
            
            spriteBatch.End();
        }

        public void DrawUiViews(SpriteBatch spriteBatch, SamplerState samplerState, params IUiView[] extra)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, samplerState);

            // UiViews.
            foreach (IUiView uiView in uiViews)
            {
                if (uiView.ViewGroupId.ContainsAny(DisabledViewGroupId)) continue;
                if (uiView.Enabled) uiView.Draw(spriteBatch);
            }

            // Extra uiViews.
            foreach (IUiView uiView in extra)
                if (uiView.Enabled) uiView.Draw(spriteBatch);
            
            spriteBatch.End();
        }

        public void DisableControllerGropId(int groupId)
        {
            DisabledControllerGroupId.Add(groupId);
        }

        public void EnableControllerGropId(int groupId)
        {
            DisabledControllerGroupId.Remove(groupId);
        }

        public void DisableViewGropId(int groupId)
        {
            DisabledViewGroupId.Add(groupId);
        }

        public void EnableViewrGropId(int groupId)
        {
            DisabledViewGroupId.Remove(groupId);
        }
    }
}
