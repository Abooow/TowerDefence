using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using TowerDefence.Factories;
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

        private async void Load()
        {
            MapManager.LoadAllMaps();

            MapManager.LoadMap(MapManager.CreateMap(MapManager.Maps[0], Game1.Graphics));

            Texture2D tileSheet;
            SpriteManager.AddTexture("TileSheet", tileSheet = Game1.ContentManager.Load<Texture2D>("tileSheet"));
            SpriteManager.AddSprite("Tower1", new Sprite(tileSheet, new Rectangle(1216, 640, 64, 64), new Vector2(32, 44)));
            SpriteManager.AddSprite("Enemy1", new Sprite(tileSheet, new Rectangle(960, 640, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("Enemy2", new Sprite(tileSheet, new Rectangle(1024, 640, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("Enemy3", new Sprite(tileSheet, new Rectangle(1088, 640, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("Enemy4", new Sprite(tileSheet, new Rectangle(1152, 640, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase1", new Sprite(tileSheet, new Rectangle(1216, 448, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase2", new Sprite(tileSheet, new Rectangle(1280, 448, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase3", new Sprite(tileSheet, new Rectangle(1344, 448, 64, 64), new Vector2(32, 32)));
            SpriteManager.AddSprite("TowerBase4", new Sprite(tileSheet, new Rectangle(1408, 448, 64, 64), new Vector2(32, 32)));

            float enemyLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Enemies);
            EnemyFactory.Add("Enemy1", new Enemy(SpriteManager.GetSprite("Enemy1"), 15f, 1f, 3f, 100f, 10f, 5f, enemyLayerDepth + 0.000f));
            EnemyFactory.Add("Enemy2", new Enemy(SpriteManager.GetSprite("Enemy2"), 15f, 1.1f, 5f, 300f, 20f, 10f, enemyLayerDepth + 0.001f));
            EnemyFactory.Add("Enemy3", new Enemy(SpriteManager.GetSprite("Enemy3"), 15f, 1.5f, 2.5f, 800f, 50f, 24f, enemyLayerDepth + 0.002f));
            EnemyFactory.Add("Enemy4", new Enemy(SpriteManager.GetSprite("Enemy4"), 15f, 3f, 1f, 1500f, 110f, 50f, enemyLayerDepth + 0.003f));
        }
    }
}
