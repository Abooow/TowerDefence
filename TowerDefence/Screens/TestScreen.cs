using System;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Controllers;
using TowerDefence.Factories;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers;
using TowerDefence.Views;
using TowerDefence.Views.UI;

namespace TowerDefence.Screens
{
    public class TestScreen : Screen
    {
        private Camera camera;
        private EnemyManager enemyManager;
        private TowerManager towerManager;
        private BulletManager bulletManager;
        private ParticleManager particleManager;
        private TowerPlacer towerPlacer;
        private SelectTowerController selectTowerController;
        private EnemySpawnerController spawnerController;

        public TestScreen()
        {
            camera = new Camera(new Vector2(0, 0))
            {
                Scale = new Vector2(0.8f)
            };

            enemyManager = new EnemyManager();
            towerManager = new TowerManager(enemyManager);
            bulletManager = new BulletManager(enemyManager);
            particleManager = new ParticleManager();
            towerPlacer = new TowerPlacer(towerManager);
            selectTowerController = new SelectTowerController(towerManager, towerPlacer, camera);
            spawnerController = new EnemySpawnerController(enemyManager);
            TowerSelectorController towerSelector = new TowerSelectorController(camera, towerPlacer, bulletManager, enemyManager, particleManager);

            // Create Tower prefabs.
            TowerFactory.Add("Tower1", new TestTower(bulletManager, enemyManager, particleManager));
            TowerFactory.Add("Tower2", new MachineGunPlaneTower(bulletManager, enemyManager, particleManager));

            enemyManager.OnEnemyReachedLastPoint += OnEnemyReachedGoal;

            // Controllers.
            controllers.Add(selectTowerController);
            controllers.Add(new MapMoverController(camera));
            controllers.Add(spawnerController);
            controllers.Add(enemyManager);
            controllers.Add(towerManager);
            controllers.Add(bulletManager);
            controllers.Add(particleManager);
            controllers.Add(towerSelector);
            //controllers.Add(new TestTowerPlacerScript(towerPlacer, TowerFactory.GetTower("Tower2"))); // TEST!

            // Views.
            views.Add(new SelectedTowerView(selectTowerController));
            views.Add(new MapView());
            views.Add(enemyManager);
            views.Add(towerManager);
            views.Add(bulletManager);
            views.Add(particleManager);

            AvailableTowersUiView availableTowersUi = new AvailableTowersUiView(new ButtonManager(), towerSelector, new Rectangle(700, 0, 300, 550));
            MouseOverlapsUI.AvailableTowersUi = availableTowersUi;
            
            // UI.
            uiViews.Add(availableTowersUi);

            spawnerController.Enabled = false;
        }

        private async void Collect()
        {
            Console.WriteLine($"Total memory before {GC.GetTotalMemory(false)}");
            await Task.Delay(5000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine($"Total memory after  {GC.GetTotalMemory(true)}");
        }

        public override void Update(float deltaTime)
        {
            if (InputManager.IsKeyJustPressed(Keys.Enter))
            {
                spawnerController.Enabled = !spawnerController.Enabled;
                if (!spawnerController.Enabled)
                    Task.Run(Collect);
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawViews(spriteBatch, SamplerState.PointClamp, camera.GetTranslationMatrix(), towerPlacer);
            DrawUiViews(spriteBatch, SamplerState.PointClamp);

            base.Draw(spriteBatch);
        }

        private void OnEnemyReachedGoal(Enemy enemy)
        {
            enemy.RemoveFromWorld();
        }
    }
}
