using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
    public class SelectTowerController : IController
    {
        public BaseTower SelectedTower { get; set; }

        private TowerManager towerManager;
        private TowerPlacer towerPlacer;

        public SelectTowerController(TowerManager towerManager, TowerPlacer towerPlacer)
        {
            this.towerManager = towerManager;
            this.towerPlacer = towerPlacer;
        }

        public void Update(float deltaTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (towerPlacer.TargetTower != null)
                    towerPlacer.PlaceTower();
                else
                {
                    SelectedTower = null;
                    foreach (BaseTower tower in towerManager.Towers)
                    {
                        Rectangle area = new Rectangle((tower.Position - tower.BaseSize.ToVector2() * 0.5f).ToPoint(), tower.BaseSize);
                        if (area.Contains(Mouse.GetState().Position))
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
