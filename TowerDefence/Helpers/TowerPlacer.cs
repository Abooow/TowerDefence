using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;
using TowerDefence.Towers;

namespace TowerDefence.Helpers
{
    public class TowerPlacer
    {
        public BaseTower TargetTower { get; set; }

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

            return canPlace = !towerManager.OverlapsAnyTowers(TargetTower, position);
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
                    position - new Vector2(TargetTower.RangeRadius) * 0.5f,
                    canPlaceColor * 0.3f);

                // Tower texture.
                if (TargetTower.Texture != null)
                {
                    TargetTower.Draw(spriteBatch, canPlace ? Color.White : Color.LightSalmon);
                }

                // Tower base area.
                Rectangle towerBase = new Rectangle((position - TargetTower.BaseSize.ToVector2() * 0.5f).ToPoint(), TargetTower.BaseSize);
                spriteBatch.Draw(SpriteManager.GetTexture("pixel"), towerBase, null, canPlaceColor * 0.4f);
            }
        }
    }
}
