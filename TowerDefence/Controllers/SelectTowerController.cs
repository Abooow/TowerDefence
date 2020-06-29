using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers;

namespace TowerDefence.Controllers
{
    public class SelectTowerController : IController
    {
        public HashSet<int> ControllerGroupId { get; }
        public bool Enabled { get; set; }
        public Tower SelectedTower { get; set; }

        private TowerManager towerManager;
        private TowerPlacer towerPlacer;
        private Camera camera;

        public SelectTowerController(TowerManager towerManager, TowerPlacer towerPlacer, Camera camera)
        {
            this.towerManager = towerManager;
            this.towerPlacer = towerPlacer;
            this.camera = camera;

            ControllerGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            if (!MouseOverlapsUI.IsMouseOverUI() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (towerPlacer.TargetTower != null)
                    towerPlacer.PlaceTower();
                else
                {
                    SelectedTower = null;
                    foreach (Tower tower in towerManager.Towers)
                    {
                        if (Circle.Contains(tower.Position, tower.BaseRadius, camera.ScreenToWorldPoint(Mouse.GetState().Position.ToVector2())))
                        {
                            SelectedTower = tower;
                            break;
                        }
                    }
                }
            }
        }
    }
}
