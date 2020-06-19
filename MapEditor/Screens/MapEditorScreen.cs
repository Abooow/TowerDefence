using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapEditor.Controllers;
using MapEditor.Models;
using MapEditor.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Screens
{
    public class MapEditorScreen : BaseScreen
    {
        private Camera camera;

        public MapEditorScreen()
        {
            camera = new Camera(new Vector2(800, 480))
            {
                Scale = new Vector2(1f)
            };

            WayPointSelector pointSelector;

            // Controllers.
            controllers.Add(new SaveMapController());
            controllers.Add(new MapMoverController(camera));
            controllers.Add(pointSelector = new WayPointSelector(camera));
            controllers.Add(new WayPointPlacer(camera, pointSelector));
            controllers.Add(new WayPointEditorController(camera, pointSelector));

            // Views.
            views.Add(new MapView());
            views.Add(new WayPointView(pointSelector));
            views.Add(new WayPointConnectionView());

            // UI.
            uiViews.Add(new PositionInfoView(camera, pointSelector));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, camera.GetTranslationMatrix());
            foreach (IView view in views) if (view.Enabled) view.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            foreach (IUIView uiView in uiViews) if (uiView.Enabled) uiView.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
