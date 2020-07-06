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
        public float BarrelLength { get; }
        public float ShootRate { get; set; }
        public float ShootTimer { get; set; }

        private ParticleManager particleManager;
        private Bullet shootBullet;

        public TestTower(BulletManager bulletManager, ParticleManager particleManager)
            : base(bulletManager, AssetManager.GetSprite("TowerBase2"), AssetManager.GetSprite("Tower1"), 26f, 250f)
        {
            this.particleManager = particleManager;

            BaseLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerBase) + extraDepth;
            TopLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerTop) + extraDepth;

            BarrelLength = 38f;
            ShootRate = 0.2f;
            shootBullet = BulletFactory.GetBullet("Bullet1");

            SearchAlgorithm = new FirstEnemySearch();
        }

        public override void Update(float deltaTime)
        {
            if (SearchAlgorithm != null)
            {
                ShootTimer -= deltaTime;
                if (SearchAlgorithm.FoundEnemy != null)
                {
                    // Rotate.
                    Vector2 delta = CalculateAimPoint(SearchAlgorithm.FoundEnemy, shootBullet) - Position;
                    Rotation = (float)Math.Atan2(delta.Y, delta.X);

                    // Shoot.
                    if (ShootTimer <= 0)
                    {
                        Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                        Vector2 bulletPosition = Position + direction * BarrelLength;
                        BulletManager.SpawnBullet(BulletFactory.GetBullet("Bullet1"), bulletPosition, direction);
                        Particle flash = new Particle(ShootRate * 0.3f, AssetManager.GetSprite("Fire1"), bulletPosition, Vector2.One, Rotation, 1f);
                        particleManager.AddParticle(flash);
                        ShootTimer = ShootRate;
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
    }
}
