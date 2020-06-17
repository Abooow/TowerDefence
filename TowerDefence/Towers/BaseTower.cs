using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Moldels;

namespace TowerDefence.Towers
{
    public abstract class BaseTower
    {
        public Vector2 Position { get; set; }
        public Point BaseSize { get; set; }
        public float RangeRadius { get; private set; }
        public float Rotation { get; set; }
        public Sprite Texture { get; set; }
        public Texture2D RangeTexture { get; internal set; }

        private float oldRange;

        public BaseTower(float range, Point size, Sprite texture)
        {
            RangeRadius = range;
            BaseSize = size;
            Texture = texture;

            RangeTexture = CreateCircleText((int)Math.Round(RangeRadius));
        }

        public virtual void Update(float deltaTime)
        {

        }

        public abstract void Draw(SpriteBatch spriteBatch, Color color);

        private Texture2D CreateCircleText(int radius)
        {
            Texture2D texture = new Texture2D(Game1.Graphics, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }
    }
}
