using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Controllers;
using TowerDefence.Managers;
using TowerDefence.Views;

namespace TowerDefence.Screens
{
    public class SelectMapScreen : Screen
    {
        public SelectMapScreen()
        {
            //MapManager.LoadMap(MapManager.CreateMap(MapManager.Maps[0], Game1.Graphics));
            AvailableMapsView mapsView = new AvailableMapsView();
            ButtonManager buttonManager = new ButtonManager();

            controllers.Add(new AvailableMapsScrollController(mapsView));
            controllers.Add(new SelectMapController(mapsView, buttonManager));
            controllers.Add(buttonManager);

            views.Add(mapsView);
            views.Add(buttonManager);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (IView view in views) if (view.Enabled) view.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
