using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Factories;
using TowerDefence.Helpers;
using TowerDefence.Interfaces;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public class TestTower : Tower, IShootTower
    {
        public float ShootRate { get; set; }
        public float ShootTimer { get; set; }
        public BulletManager BulletManager { get; set; }
        public EnemyManager EnemyManager { get; set; }
        public ISearchAlgorithm SearchAlgorithm { get; set; }

        public float barrelLength;
        private ParticleManager particleManager;
        private Bullet shootBullet;

        public TestTower(BulletManager bulletManager, EnemyManager enemyManager, ParticleManager particleManager)
            : base(AssetManager.GetSprite("TowerBase2"), AssetManager.GetSprite("Tower1"), 26f, 250f)
        {
            BulletManager =  bulletManager;
            EnemyManager = enemyManager;
            this.particleManager = particleManager;

            BaseLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerBase) + extraDepth;
            TopLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerTop) + extraDepth;

            barrelLength = 38f;
            ShootRate = 0.2f;
            shootBullet = BulletFactory.GetBullet("Bullet2");
            SearchAlgorithm = new FirstEnemySearch();
        }

        public override void Update(float deltaTime)
        {
            if (SearchAlgorithm != null)
            {
                ShootTimer -= deltaTime;
                FindEnemy();

                if (SearchAlgorithm.FoundEnemy != null)
                {
                    // Rotate.
                    Vector2 delta = CalculateAimPoint(SearchAlgorithm.FoundEnemy, shootBullet) - Position;
                    Rotation = (float)Math.Atan2(delta.Y, delta.X);

                    // Shoot.
                    if (ShootTimer <= 0)
                    {
                        Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                        Vector2 bulletPosition = Position + direction * barrelLength;
                        BulletManager.SpawnBullet(shootBullet.Duplicate(), bulletPosition, direction);
                        Particle flash = new Particle(ShootRate * 0.3f, AssetManager.GetSprite("Fire1"), bulletPosition, Vector2.One, Rotation, 1f);
                        particleManager.AddParticle(flash);
                        ShootTimer = ShootRate;
                    }
                }
            }
        }

        public void FindEnemy()
        {
            SearchAlgorithm.FoundEnemy = null;
            SearchAlgorithm.Tower = this;
            EnemyManager.Query(Position, RangeRadius + EnemyFactory.LargestEnemyHitboxRadius, SearchAlgorithm.FindEnemies);
            SearchAlgorithm.Tower = null;
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

        public override void UIDraw(SpriteBatch spriteBatch, Color color)
        {
            Draw(spriteBatch, color);
        }

        public override Vector2 GetPosition() => Position;

        public override Tower Duplicate()
        {
            return new TestTower(BulletManager, EnemyManager, particleManager);
        }
    }
}
