using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers
{
    public abstract class BaseTower
    {
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

        private float oldRange;

        public BaseTower(Sprite baseTexture, Sprite topTexture, float baseRadius, float range)
        {
            BaseTexture = baseTexture;
            TopTexture = topTexture;
            BaseRadius = baseRadius;
            RangeRadius = range;

            RangeTexture = Circle.GetTexture(Game1.Graphics, (int)Math.Round(range * 2f));
            BaseRangeTexture = Circle.GetTexture(Game1.Graphics, (int)Math.Round(baseRadius * 2f));
        }

        public virtual void Update(float deltaTime)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch, Color color);
    }
}
