using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Controllers;
using TowerDefence.Helpers;

namespace TowerDefence.Moldels
{
    public class Enemy : SpaceUnit
    {
        public static float ExtraDepth;

        public Sprite Sprite { get; set; }
        public float HitboxRadius { get; set; }
        public float Scale { get; set; }
        public float Speed { get; set; }
        public float Health { get; set; }
        public float Armor { get; set; }
        public float Damage { get; set; }
        public float Rotation { get; set; }
        public float LayerDepth { get; set; }
        public EnemyAiController AiController { get; }
        public bool HaveReachedLastWayPoint { get; private set; }

        public Enemy(SpacePartitioner world, Sprite sprite, float hitboxRadius, float scale, float speed, float health, float armor, float damage, float layerDepth)
            : base(world)
        {
            Sprite = sprite;
            HitboxRadius = hitboxRadius;
            Scale = scale;
            Speed = speed;
            Health = health;
            Armor = armor;
            Damage = damage;
            LayerDepth = layerDepth + ExtraDepth;

            AiController = new EnemyAiController(this);
            ExtraDepth += 0.00001f;
        }

        public bool Update(float deltaTime)
        {
            AiController.Update(deltaTime);
            HaveReachedLastWayPoint = AiController.HaveReachedLastWayPoint;

            return Health <= 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite,
                Position,
                Sprite.SourceRect,
                Color.White,
                Rotation,
                Sprite.Origin,
                Scale,
                SpriteEffects.None,
                LayerDepth);
        }

        public void TakeDamage(float damage)
        {
            float damageMultiplier;
            if (Armor >= 0) damageMultiplier = 100f / (100 + Armor);
            else damageMultiplier = 2f - (100f / (100 - Armor));

            Health -= damage * damageMultiplier;
        }
    }
}
