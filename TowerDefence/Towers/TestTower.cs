using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Factories;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public class TestTower : Tower
    {
        private float shootRate;
        private float timer;
        private Bullet bullet;

        public TestTower(BulletManager bulletManager)
            : base(bulletManager, AssetManager.GetSprite("TowerBase2"), AssetManager.GetSprite("Tower1"), 26f, 250f)
        {
            BaseLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerBase) + extraDepth;
            TopLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerTop) + extraDepth;

            shootRate = 0.2f;
            bullet = BulletFactory.GetBullet("Bullet1");

            SearchAlgorithm = new FirstEnemySearch();
        }

        public override void Update(float deltaTime, List<List<SpacePartitioner<Enemy>.PointData>> nearbyEnemies)
        {
            if (SearchAlgorithm != null)
            {
                Enemy foundEnemy = SearchAlgorithm.FindEnemy(this, nearbyEnemies);

                timer -= deltaTime;
                if (foundEnemy != null)
                {
                    // Rotate.
                    Vector2 delta = CalculateAimPoint(foundEnemy) - Position;
                    Rotation = (float)Math.Atan2(delta.Y, delta.X);

                    // Shoot.
                    if (timer <= 0)
                    {
                        Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                        BulletManager.SpawnBullet(BulletFactory.GetBullet("Bullet1"), Position, direction);
                        timer = shootRate;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(BaseTexture, Position, BaseTexture.SourceRect, color, 0f, BaseTexture.Origin, 1f, SpriteEffects.None, BaseLayerDepth);

            spriteBatch.Draw(
                TopTexture,
                Position,
                TopTexture.SourceRect,
                color,
                Rotation,
                TopTexture.Origin,
                Vector2.One,
                SpriteEffects.None,
                TopLayerDepth);
        }

        private Vector2 CalculateAimPoint(Enemy target)
        {
            float distance = Vector2.Distance(Position, target.Position);
            float travelTime = distance / bullet.Speed;
            Vector2 targetVelocity = new Vector2((float)Math.Cos(target.Rotation), (float)Math.Sin(target.Rotation)) * target.Speed;

            return target.Position + targetVelocity  * travelTime;
        }
    }
}
