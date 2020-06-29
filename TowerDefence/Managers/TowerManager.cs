using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.Controllers;
using TowerDefence.Factories;
using TowerDefence.Helpers;
using TowerDefence.Moldels;
using TowerDefence.Towers;
using TowerDefence.Views;

namespace TowerDefence.Managers
{
    public class TowerManager : IController, IView
    {
        public HashSet<int> ControllerGroupId { get; }
        public HashSet<int> ViewGroupId { get; }
        public bool Enabled { get; set; }
        public List<Tower> Towers { get; }

        private EnemyManager enemyManager;
        //private SelectTowerController selectTowerController;

        public TowerManager(EnemyManager enemyManager/*, SelectTowerController selectTowerController*/)
        {
            this.enemyManager = enemyManager;
            //this.selectTowerController = selectTowerController;

            Towers = new List<Tower>();

            ControllerGroupId = new HashSet<int>();
            ViewGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void AddTower(Tower tower)
        {
            if (!Towers.Contains(tower)) Towers.Add(tower);
        }

        public bool OverlapsAnyTowers(Tower tower)
        {
            if (tower == null) return false;

            foreach (Tower otherTower in Towers)
                if (Circle.Intercects(tower.Position, tower.BaseRadius, otherTower.Position, otherTower.BaseRadius)) return true;

            return false;
        }

        public bool CanPlaceOnMap(Tower tower, Map map)
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
            foreach (Tower tower in Towers)
            {
                tower.SearchAlgorithm.FoundEnemy = null;
                tower.SearchAlgorithm.Tower = tower;
                enemyManager.Query(tower.Position, tower.RangeRadius + EnemyFactory.LargestEnemyHitboxRadius, tower.SearchAlgorithm.FindEnemies);
                tower.Update(deltaTime);
                tower.SearchAlgorithm.Tower = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in Towers)
            {
                /*if (!(tower == selectTowerController.SelectedTower)) */tower.Draw(spriteBatch, Color.White);
            }
        }
    }
}
