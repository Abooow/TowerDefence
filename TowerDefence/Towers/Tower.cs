using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Helpers;
using TowerDefence.Interfaces;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public abstract class Tower : IPositionable, IDuplicatable<Tower>
    {
        internal static float extraDepth;

        public Vector2 Position { get; set; }
        public float BaseRadius { get; set; }
        public float RangeRadius { get; private set; }
        public float Rotation { get; set; }
        public float BaseLayerDepth { get; set; }
        public float TopLayerDepth { get; set; }
        public Sprite BaseTexture { get; set; }
        public Sprite TopTexture { get; set; }
        public Texture2D RangeTexture { get; internal set; }
        public Texture2D BaseRangeTexture { get; internal set; }

        public Tower(Sprite baseTexture, Sprite topTexture, float baseRadius, float range)
        {
            BaseTexture = baseTexture;
            TopTexture = topTexture;
            BaseRadius = baseRadius;
            RangeRadius = range;

            Texture2D rangeTextue = AssetManager.GetTexture($"RangeTexture{(int)Math.Round(range * 2f)}");
            Texture2D baseRangeTextue = AssetManager.GetTexture($"RangeTexture{(int)Math.Round(baseRadius * 2f)}");
            if (rangeTextue == null)
            {
                AssetManager.AddTexture($"RangeTexture{(int)Math.Round(range * 2f)}", Circle.GetTexture(Game1.Graphics, (int)Math.Round(range * 2f)));
                rangeTextue = AssetManager.GetTexture($"RangeTexture{(int)Math.Round(range * 2f)}");
            }
            if (baseRangeTextue == null)
            {
                AssetManager.AddTexture($"RangeTexture{(int)Math.Round(baseRadius * 2f)}", Circle.GetTexture(Game1.Graphics, (int)Math.Round(baseRadius * 2f)));
                baseRangeTextue = AssetManager.GetTexture($"RangeTexture{(int)Math.Round(baseRadius * 2f)}");
            }

            RangeTexture = rangeTextue;
            BaseRangeTexture = baseRangeTextue;

            extraDepth += 0.000001f;
        }

        public abstract Vector2 GetPosition();

        public abstract void Update(float deltaTime);

        public abstract void Draw(SpriteBatch spriteBatch, Color color);

        public abstract void UIDraw(SpriteBatch spriteBatch, Color color);

        protected Vector2 CalculateAimPoint(Enemy target, Bullet bullet)
        {
            float distance = Vector2.Distance(Position, target.Position);
            float travelTime = distance / bullet.Speed;
            Vector2 targetVelocity = new Vector2((float)Math.Cos(target.Rotation), (float)Math.Sin(target.Rotation)) * target.Speed;

            return target.Position + targetVelocity * travelTime;
        }

        public abstract Tower Duplicate();
    }
}
