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
            ScreenManager.ChangeScreen(new SelectMapScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        private void Load()
        {
            // Load Maps.
            MapManager.LoadAllMaps();
            for (int i = 0; i < MapManager.TotalLoadedMaps; i++)
            {
                AssetManager.AddTexture($"Thumbnail{i}", AssetManager.LoadTexture2D(Game1.Graphics, MapManager.Maps[i].ThumbnailPath));
            }

            // Load Textures.
            Texture2D tileSheet;
            AssetManager.AddTexture("TileSheet", tileSheet = Game1.ContentManager.Load<Texture2D>("tileSheet"));
            Texture2D pixel = new Texture2D(Game1.Graphics, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            AssetManager.AddTexture("Pixel", pixel);

            // Load Fonts.
            AssetManager.AddFont("BaseFont", Game1.ContentManager.Load<SpriteFont>("BaseFont"));

            // Load Sprites.
            AssetManager.AddSprite("TowerBase1", new Sprite(tileSheet, new Rectangle(1216, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase2", new Sprite(tileSheet, new Rectangle(1280, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase3", new Sprite(tileSheet, new Rectangle(1344, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("TowerBase4", new Sprite(tileSheet, new Rectangle(1408, 448, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Tower1", new Sprite(tileSheet, new Rectangle(1216, 640, 64, 64), new Vector2(20, 33)));
            AssetManager.AddSprite("Tower2", new Sprite(tileSheet, new Rectangle(1088, 704, 64, 64), new Vector2(37, 33)));
            AssetManager.AddSprite("PlaneShadow1", new Sprite(tileSheet, new Rectangle(1088, 768, 64, 64), new Vector2(37, 33)));
            AssetManager.AddSprite("Enemy1", new Sprite(tileSheet, new Rectangle(960, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Enemy2", new Sprite(tileSheet, new Rectangle(1024, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Enemy3", new Sprite(tileSheet, new Rectangle(1088, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Enemy4", new Sprite(tileSheet, new Rectangle(1152, 640, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Bullet1", new Sprite(tileSheet, new Rectangle(1216, 704, 64, 64), new Vector2(32, 32)));
            AssetManager.AddSprite("Bullet2", new Sprite(tileSheet, new Rectangle(1344, 640, 64, 64), new Vector2(42, 30)));
            AssetManager.AddSprite("Fire1", new Sprite(tileSheet, new Rectangle(1216, 768, 64, 64), new Vector2(20, 29)));
            
            // Create Enemy prefabs.
            float enemyLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Enemies);
            EnemyFactory.Add("Enemy1", new Enemy(null, AssetManager.GetSprite("Enemy1"), hitboxRadius: 15f, scale: 1f, speed: 280f, health: 100f, armor: 10f, damage: 5f, enemyLayerDepth + 0.000f));
            EnemyFactory.Add("Enemy2", new Enemy(null, AssetManager.GetSprite("Enemy2"), 15f, 1.1f, 450f, 300f, 20f, 10f, enemyLayerDepth + 0.001f));
            EnemyFactory.Add("Enemy3", new Enemy(null, AssetManager.GetSprite("Enemy3"), 20f, 1.5f, 270f, 1000f, 110f, 24f, enemyLayerDepth + 0.002f));
            EnemyFactory.Add("Enemy4", new Enemy(null, AssetManager.GetSprite("Enemy4"), 30f, 3f, 100f, 1500f, 110f, 50f, enemyLayerDepth + 0.003f));

            // Create Bullet prefabs.
            float bulletLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Bullets);
            BulletFactory.Add("Bullet1", new Bullet(AssetManager.GetSprite("Bullet1"), 400f, 1100f, 100, bulletLayerDepth + 0.000f));
            BulletFactory.Add("Bullet2", new Bullet(AssetManager.GetSprite("Bullet2"), 400f, 1100f, 280f, bulletLayerDepth + 0.000f));
        }
    }
}
