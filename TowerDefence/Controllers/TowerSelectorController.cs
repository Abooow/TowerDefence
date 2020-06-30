using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers;

namespace TowerDefence.Controllers
{
    public class TowerSelectorController : IController
    {
        public HashSet<int> ControllerGroupId { get; }
        public bool Enabled { get; set; }

        private Camera camera;
        private TowerPlacer towerPlacer;
        private BulletManager bulletManager;
        private ParticleManager particleManager;

        public TowerSelectorController(Camera camera, TowerPlacer towerPlacer, BulletManager bulletManager, ParticleManager particleManager)
        {
            this.camera = camera;
            this.towerPlacer = towerPlacer;
            this.bulletManager = bulletManager;
            this.particleManager = particleManager;

            ControllerGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            if (towerPlacer.HaveTargetTower)
            {
                towerPlacer.MoveTower(camera.ScreenToWorldPoint(Mouse.GetState().Position.ToVector2()));

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    towerPlacer.PlaceTower();
                }
                else if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    towerPlacer.TargetTower = null;
                }
            }

        }

        public void OnTowerButtonClicked(object obj, EventArgs args)
        {
            Tower tower = null;
            string towerName = ((Button)obj).AttachedObject as string;

            switch (towerName)
            {
                case "Tower1":
                    tower = new TestTower(bulletManager, particleManager);
                    break;
            }

            towerPlacer.TargetTower = tower;
        }
    }
}
