using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers
{
    public class TestTower : BaseTower
    {
        public TestTower()
            : base(SpriteManager.GetSprite("TowerBase2"), SpriteManager.GetSprite("Tower1"), 26f, 250f)
        {
            BaseLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerBase);
            TopLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerTop);
        }

        public override void Update(float deltaTime)
        {
            Rotation += (float)Math.PI / 180f;
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
    }
}
