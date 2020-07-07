using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Factories;
using TowerDefence.Interfaces;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public class MachineGunPlaneTower : Tower, IShootTower
    {
        public float ShootRate { get; set; }
        public float ShootTimer { get; set; }
        public BulletManager BulletManager { get; set; }
        public EnemyManager EnemyManager { get; set; }
        public ISearchAlgorithm SearchAlgorithm { get; set; }

        private float barrelLength;
        private ParticleManager particleManager;
        private Bullet shootBullet;

        private Sprite shadowSprite;

        private Vector2 planeOffsetPosition;
        private float rotationAngle;
        private float rotationRadius;
        private float planeRotationSpeed;

        public MachineGunPlaneTower(BulletManager bulletManager, EnemyManager enemyManager, ParticleManager particleManager)
            : base(AssetManager.GetSprite("TowerBase1"), AssetManager.GetSprite("Tower2"), 26f, 150f)
        {
            BulletManager = bulletManager;
            EnemyManager = enemyManager;
            this.particleManager = particleManager;

            BaseLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerBase) + extraDepth;
            TopLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Flying) + extraDepth;

            barrelLength = 10f;
            ShootRate = 0.1f;
            shootBullet = BulletFactory.GetBullet("Bullet1");
            SearchAlgorithm = new FirstEnemySearch();

            shadowSprite = AssetManager.GetSprite("PlaneShadow1");

            rotationRadius = 150f;
            planeRotationSpeed = (float)Math.PI * 0.004f;
        }

        public override void Update(float deltaTime)
        {
            // Rotate.
            rotationAngle += planeRotationSpeed;
            Rotation = rotationAngle + (float)Math.PI * 0.5f;
            
            planeOffsetPosition = new Vector2((float)Math.Cos(rotationAngle), (float)Math.Sin(rotationAngle)) * rotationRadius;

            if (SearchAlgorithm != null)
            {
                ShootTimer -= deltaTime;
                FindEnemy();
                if (SearchAlgorithm.FoundEnemy != null)
                {
                    // Shoot.
                    if (ShootTimer <= 0)
                    {
                        Vector2 direction = (CalculateAimPoint(SearchAlgorithm.FoundEnemy, shootBullet) - GetPosition());
                        direction.Normalize();
                        Vector2 bulletPosition = planeOffsetPosition + Position + direction * barrelLength;

                        BulletManager.SpawnBullet(shootBullet.Duplicate(), bulletPosition, direction);
                        
                        Particle flash = new Particle(ShootRate * 0.3f, AssetManager.GetSprite("Fire1"), bulletPosition, Vector2.One, (float)Math.Atan2(direction.Y, direction.X), 1f);
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
            EnemyManager.Query(GetPosition(), RangeRadius + EnemyFactory.LargestEnemyHitboxRadius, SearchAlgorithm.FindEnemies);
            SearchAlgorithm.Tower = null;
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(BaseTexture, Position, BaseTexture.SourceRect, color, 0f, BaseTexture.Origin, 1f, SpriteEffects.None, BaseLayerDepth);

            // Shadow.
            spriteBatch.Draw(
                shadowSprite,
                GetPosition() + new Vector2(-10f, 12f),
                shadowSprite.SourceRect,
                color,
                Rotation,
                shadowSprite.Origin,
                Vector2.One,
                SpriteEffects.None,
                TopLayerDepth - 0.0001f);

            // Plane.
            spriteBatch.Draw(
                TopTexture,
                GetPosition(),
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

        public override Vector2 GetPosition() => Position + planeOffsetPosition;

        public override Tower Duplicate() 
        {
            return new MachineGunPlaneTower(BulletManager, EnemyManager, particleManager);
        }
    }
}
