﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Towers;

namespace TowerDefence.Controllers
{
    public class TestTowerPlacerScript : IController
    {
        public bool Enabled { get; set; }

        public TestTowerPlacerScript(TowerPlacer towerPlacer, BulletManager bulletManager)
        {
            Tower towerInfo = new TestTower(bulletManager);

            for (int y = 0; y < MapManager.LoadedMap.PermittedTowerPlacementTexture.Height / (towerInfo.BaseRadius * 2) / 2; y++)
            {
                for (int x = 0; x < MapManager.LoadedMap.PermittedTowerPlacementTexture.Width / (towerInfo.BaseRadius * 2) / 2; x++)
                {
                    towerPlacer.TargetTower = new TestTower(bulletManager);
                    towerPlacer.MoveTower(new Vector2(x * towerInfo.BaseRadius * 2, y * towerInfo.BaseRadius * 2));
                    towerPlacer.PlaceTower();
                }
            }
        }

        public void Update(float deltaTime)
        {
        }
    }
}
