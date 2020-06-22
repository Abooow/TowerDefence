using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Helpers;

namespace TowerDefence.Moldels
{
    public class Bullet
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float MaxDistance { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }
        public float LayerDepth { get; set; }

        private float traveledDistance;
        private float rotation;

        public Bullet(Sprite sprite, float maxDistance, float speed, float damage, float layerDepth)
        {
            Sprite = sprite;
            MaxDistance = maxDistance;
            Speed = speed;
            Damage = damage;
            LayerDepth = layerDepth;
        }

        public bool Update(float deltaTime, List<WorldDivider<Enemy>.PointData> nearbyEnemies)
        {
            // Move.
            float distance = Speed * deltaTime;
            Position += Direction * distance;
            traveledDistance += distance;

            if (traveledDistance >= MaxDistance) return true;

            // Rotate.
            rotation = (float)Math.Atan2(Direction.Y, Direction.X);

            // Check for collisions.
            foreach (var enemyPoint in nearbyEnemies)
            {
                if (Circle.Contains(enemyPoint.Point.Position, enemyPoint.Point.HitboxRadius, Position))
                {
                    enemyPoint.Point.TakeDamage(Damage);
                    return true;
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite,
                Position,
                Sprite.SourceRect,
                Color.White,
                rotation,
                Sprite.Origin,
                1f,
                SpriteEffects.None,
                LayerDepth);
        }
    }
}
