using MapEditor.Helpers;
using MapEditor.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace MapEditor.Screens
{
    public class LoadScreen : BaseScreen
    {
        public LoadScreen()
        {
            Load();
        }

        public override void Update(float deltaTime)
        {
            ScreenManager.ChangeScreen(new MapEditorScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        private void Load()
        {
            WaypointManager.DefaultWaypointRadius = 13f;
            WaypointManager.DefaultWaypointTexture = Circle.GetTexture(Game1.Graphics, 26);

            MapManager.Load(Game1.Graphics, @"C:\Users\danie\source\gitRepos\c#\TowerDefence\TowerDefence\Maps\Map1.png", @"C:\Users\danie\source\gitRepos\c#\TowerDefence\TowerDefence\Maps\Map1PermittedTowerPlaces.png");
        }
    }
}
