using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Controllers;

namespace TowerDefence.Moldels
{
    public class Enemy
    {
        public Sprite Sprite { get; set; }
        public float HitboxRadius { get; set; }
        public float Scale { get; set; }
        public float Speed { get; set; }
        public float Health { get; set; }
        public float Armor { get; set; }
        public float Damage { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float LayerDepth { get; set; }
        public bool HaveReachedLastWayPoint { get; private set; }

        private EnemyAiController aiController;

        public Enemy(Sprite sprite, float hitboxRadius, float scale, float speed, float health, float armor, float damage, float layerDepth)
        {
            Sprite = sprite;
            HitboxRadius = hitboxRadius;
            Scale = scale;
            Speed = speed;
            Health = health;
            Armor = armor;
            Damage = damage;
            LayerDepth = layerDepth;

            aiController = new EnemyAiController(this);
        }

        public void Update(float deltaTime)
        {
            aiController.Update(deltaTime);
            HaveReachedLastWayPoint = aiController.HaveReachedLastWayPoint;
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

        public bool TakeDamage(float damage)
        {
            float damageMultiplier;
            if (Armor >= 0) damageMultiplier = 100f / (100 + Armor);
            else damageMultiplier = 2f - (100f / (100 - Armor));

            Health -= damage * damageMultiplier;

            return Health <= 0;
        }
    }
}
