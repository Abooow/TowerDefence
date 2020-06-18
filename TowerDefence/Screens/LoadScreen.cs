using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Screens
{
    public class LoadScreen : BaseScreen
    {
        public LoadScreen()
        {
            Load();
        }

        public override void Update(float deltaTime)
        {
            ScreenManager.ChangeScreen(new TestScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        private void Load()
        {
            MapManager.LoadAllMaps("*.mapData");

            MapManager.LoadMap(MapManager.CreateMap(MapManager.Maps[0], Game1.Graphics));

            Texture2D tileSheet;
            SpriteManager.AddTexture("TileSheet", tileSheet = Game1.ContentManager.Load<Texture2D>("tileSheet"));
            SpriteManager.AddSprite("Tower1",     new Sprite(tileSheet, new Rectangle(1216, 640, 64, 64), new Vector2(32, 44)));
            SpriteManager.AddSprite("TowerBase1", new Sprite(tileSheet, new Rectangle(1216, 448, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase2", new Sprite(tileSheet, new Rectangle(1280, 448, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase3", new Sprite(tileSheet, new Rectangle(1344, 448, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase4", new Sprite(tileSheet, new Rectangle(1408, 448, 64, 64), new Vector2(32, 32)));
        }
    }
}
