using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using TowerDefence.Factories;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Screens
{
    public class LoadScreen : Screen
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
            // Load Maps.
            MapManager.LoadAllMaps();
            MapManager.LoadMap(MapManager.CreateMap(MapManager.Maps[1], Game1.Graphics));

            // Load Textures.
            Texture2D pixel = new Texture2D(Game1.Graphics, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            AssetManager.AddTexture("Pixel", pixel);

            // Load Fonts.
            AssetManager.AddFont("BaseFont", Game1.ContentManager.Load<SpriteFont>("BaseFont"));

            // Load Sprites.
            Texture2D tileSheet;
            AssetManager.AddTexture("TileSheet", tileSheet = Game1.ContentManager.Load<Texture2D>("tileSheet"));
            AssetManager.AddSprite("Tower1", new Sprite(tileSheet, new Rectangle(1216, 640, 64, 64), new Vector2(20, 33)));
            AssetManager.AddSprite("Enemy1", new Sprite(tileSheet, new Rectangle(960, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Enemy2", new Sprite(tileSheet, new Rectangle(1024, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Enemy3", new Sprite(tileSheet, new Rectangle(1088, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Enemy4", new Sprite(tileSheet, new Rectangle(1152, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Bullet1", new Sprite(tileSheet, new Rectangle(1216, 704, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase1", new Sprite(tileSheet, new Rectangle(1216, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase2", new Sprite(tileSheet, new Rectangle(1280, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase3", new Sprite(tileSheet, new Rectangle(1344, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase4", new Sprite(tileSheet, new Rectangle(1408, 448, 64, 64), new Vector2(32, 32)));
            
            // Create Enemy prefabs.
            float enemyLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Enemies);
            EnemyFactory.Add("Enemy1", new Enemy(AssetManager.GetSprite("Enemy1"), 15f, 1f, 280f, 100f, 10f, 5f, enemyLayerDepth + 0.000f));
            EnemyFactory.Add("Enemy2", new Enemy(AssetManager.GetSprite("Enemy2"), 15f, 1.1f, 450f, 300f, 20f, 10f, enemyLayerDepth + 0.001f));
            EnemyFactory.Add("Enemy3", new Enemy(AssetManager.GetSprite("Enemy3"), 15f, 1.5f, 210f, 800f, 50f, 24f, enemyLayerDepth + 0.002f));
            EnemyFactory.Add("Enemy4", new Enemy(AssetManager.GetSprite("Enemy4"), 15f, 3f, 100f, 1500f, 110f, 50f, enemyLayerDepth + 0.003f));

            // Create Bullet prefabs.
            float bulletLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Bullets);
            BulletFactory.Add("Bullet1", new Bullet(AssetManager.GetSprite("Bullet1"), 400f, 1000f, 200f, bulletLayerDepth + 0.000f));
        }
    }
}
