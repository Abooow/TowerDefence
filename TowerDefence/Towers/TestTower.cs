using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers
{
    public class TestTower : BaseTower
    {
        float towerBaseLayerDepth;
        float towerTopLayerDepth;

        public TestTower()
            : base(100f, new Point(52, 52), SpriteManager.GetSprite("Tower1"))
        {
            towerBaseLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerBase);
            towerTopLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.TowerTop);
        }

        public override void Update(float deltaTime)
        {
            Rotation += (float)Math.PI / 180f;
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            Sprite towerBase = SpriteManager.GetSprite("TowerBase2");
            spriteBatch.Draw(towerBase.Texture, Position, towerBase.SourceRect, color, 0f, towerBase.Origin, 1f, SpriteEffects.None, towerBaseLayerDepth);

            spriteBatch.Draw(
                Texture,
                Position,
                Texture.SourceRect,
                color,
                Rotation,
                Texture.Origin,
                Vector2.One,
                SpriteEffects.None,
                towerTopLayerDepth);
        }
    }
}
