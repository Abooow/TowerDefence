using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Moldels
{
    public class Particle
    {
        public delegate void UpdateEvent(Particle particle, float deltaTime);

        public UpdateEvent OnUpdate;
        public float TimeToBeAlive;
        public Sprite Sprite;
        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;
        public float LayerDepth;
        public Color Color;

        private float timeLived;

        public Particle(float timeToBeAlive, Sprite sprite, Vector2 position, Vector2 scale, float rotation = 0f, float layerDepth = 0.5f)
        {
            TimeToBeAlive = timeToBeAlive;
            Sprite = sprite;
            Position = position;
            Scale = scale;
            Rotation = rotation;
            LayerDepth = layerDepth;
            Color = Color.White;
        }

        public bool Update(float deltaTime)
        {
            timeLived += deltaTime;
            if (timeLived >= TimeToBeAlive) return true;

            OnUpdate?.Invoke(this, deltaTime);
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
            {
                spriteBatch.Draw(
                    Sprite,
                    Position,
                    Sprite.SourceRect,
                    Color,
                    Rotation,
                    Sprite.Origin,
                    Scale,
                    SpriteEffects.None,
                    LayerDepth);
            }
        }
    }
}
