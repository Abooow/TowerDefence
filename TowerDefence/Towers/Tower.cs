using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public abstract class Tower : IPositionable
    {
        internal static float extraDepth;

        public BulletManager BulletManager { get; }
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
        public ISearchAlgorithm SearchAlgorithm { get; set; }

        private float oldRange;

        public Tower(BulletManager bulletManager, Sprite baseTexture, Sprite topTexture, float baseRadius, float range)
        {
            BulletManager = bulletManager;
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

            extraDepth += 0.0000001f;
        }

        public virtual void Update(float deltaTime, List<List<SpacePartitioner<Enemy>.PointData>> nearbyEnemies)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch, Color color);
    }
}
