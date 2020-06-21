using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;
using TowerDefence.Towers;

namespace TowerDefence.Helpers
{
    public class TowerPlacer
    {
        public Tower TargetTower { get; set; }

        public bool HaveTargetTower => TargetTower != null;

        private TowerManager towerManager;
        private Vector2 position;
        private bool canPlace;

        public TowerPlacer(TowerManager towerManager)
        {
            this.towerManager = towerManager;
        }

        public bool MoveTower(Vector2 position)
        {
            this.position = position;
            TargetTower.Position = position;

            return canPlace = !towerManager.OverlapsAnyTowers(TargetTower) && towerManager.CanPlaceOnMap(TargetTower, MapManager.LoadedMap);
        }

        public void PlaceTower()
        {
            if (canPlace && TargetTower != null)
            {
                towerManager.AddTower(TargetTower);
                TargetTower = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (TargetTower != null)
            {
                Color canPlaceColor = canPlace ? Color.Green : Color.Red;

                // Tower range.
                spriteBatch.Draw(
                    TargetTower.RangeTexture,
                    position - new Vector2(TargetTower.RangeRadius),
                    null,
                    canPlaceColor * 0.3f,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    TargetTower.BaseLayerDepth - 0.001f);

                // Tower texture.
                if (TargetTower.TopTexture != null)
                {
                    TargetTower.Draw(spriteBatch, canPlace ? Color.White : Color.LightSalmon);
                }

                // Tower base area.
                spriteBatch.Draw(TargetTower.BaseRangeTexture, 
                    position - new Vector2(TargetTower.BaseRadius),
                    null, 
                    canPlaceColor * 0.4f,
                    0f, 
                    Vector2.Zero, 
                    1f, 
                    SpriteEffects.None, 
                    TargetTower.BaseLayerDepth + 0.001f);
            }
        }
    }
}
