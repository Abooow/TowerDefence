using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TowerDefence.Helpers;
using TowerDefence.Moldels;
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

        public bool OverlapsAnyTowers(BaseTower tower)
        {
            if (tower == null) return false;

            foreach (BaseTower otherTower in Towers)
                if (Circle.Intercects(tower.Position, tower.BaseRadius, otherTower.Position, otherTower.BaseRadius)) return true;

            return false;
        }

        public bool CanPlaceOnMap(BaseTower tower, Map map)
        {
            if (tower == null) return false;
            if (map == null || map.PermittedTowerPlacementTexture == null) return true;

            int diameter = (int)tower.BaseRadius * 2;
            Point position = (tower.Position - new Vector2(tower.BaseRadius)).ToPoint();

            if (position.X < 0) position.X = 0;
            if (position.Y < 0) position.Y = 0;
            if (position.X + diameter > map.PermittedTowerPlacementTexture.Width) position.X = map.PermittedTowerPlacementTexture.Width - diameter;
            if (position.Y + diameter > map.PermittedTowerPlacementTexture.Height) position.Y = map.PermittedTowerPlacementTexture.Height - diameter;

            // Get colorData.
            //Color[] color = new Color[diameter * diameter];
            //for (int x = 0; x < diameter; x++)
            //    for (int y = 0; y < diameter; y++)
            //        color[x + y * diameter] = map.PermittedTowerPlacementTexture.GetPixel[x + position.X + (y + position.Y) * map.PermittedTowerPlacementTexture.Width];

            float radius = tower.BaseRadius;
            float radiusSq = radius * radius;
            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    Vector2 pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= radiusSq)
                    {
                        if (map.PermittedTowerPlacementTexture.GetPixel(position.X + x, position.Y + y).R == 255) return false;
                    }
                }
            }

            return true;
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
