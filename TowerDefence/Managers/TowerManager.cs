using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TowerDefence.Towers;

namespace TowerDefence.Managers
{
    public class TowerManager
    {
        public List<BaseTower> Towers { get; }

        public TowerManager()
        {
            Towers = new List<BaseTower>();
        }

        public void AddTower(BaseTower tower)
        {
            if (!Towers.Contains(tower)) Towers.Add(tower);
        }

        public bool OverlapsAnyTowers(BaseTower tower, Vector2 position)
        {
            if (tower == null) return false;

            Rectangle towerRect = new Rectangle(position.ToPoint(), tower.BaseSize);

            foreach (BaseTower otherTower in Towers)
            {
                Rectangle otherRect = new Rectangle(otherTower.Position.ToPoint(), otherTower.BaseSize);
                if (towerRect.Intersects(otherRect)) return true;
            }

            return false;
        }

        public void Update(float deltaTime)
        {
            foreach (BaseTower tower in Towers)
            {
                tower.Update(deltaTime);
            }
        }
    }
}
