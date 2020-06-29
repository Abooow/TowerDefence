using System;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Controllers;
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
            towerPlacer = new TowerPlacer(towerManager);
            selectTowerController = new SelectTowerController(towerManager, towerPlacer, camera);
            TowerSelectorController towerSelector;

            enemyManager.OnEnemyReachedLastPoint += OnEnemyReachedGoal;

            // Controllers.
            controllers.Add(selectTowerController);
            controllers.Add(new MapMoverController(camera));
            controllers.Add(spawnerController = new EnemySpawnerController(enemyManager));
            controllers.Add(enemyManager);
            controllers.Add(towerManager);
            controllers.Add(bulletManager);
            controllers.Add(towerSelector = new TowerSelectorController(camera, towerPlacer, bulletManager));
            //controllers.Add(new TestTowerPlacerScript(towerPlacer, bulletManager)); // TEST!

            // Views.
            views.Add(new SelectedTowerView(selectTowerController));
            views.Add(new MapView());
            views.Add(enemyManager);
            views.Add(towerManager);
            views.Add(bulletManager);

            AvailableTowersUiView availableTowersUi = new AvailableTowersUiView(new ButtonManager(), towerSelector, new Rectangle(700, 0, 300, 550));
            MouseOverlapsUI.AvailableTowersUi = availableTowersUi;
            
            // UI.
            uiViews.Add(availableTowersUi);

            spawnerController.Enabled = false;
        }

        async void Collect()
        {
            Console.WriteLine($"Total memory before {GC.GetTotalMemory(false)}");
            await Task.Delay(5000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine($"Total memory after  {GC.GetTotalMemory(true)}");
        }

        KeyboardState s;
        public override void Update(float deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && s.IsKeyUp(Keys.Enter))
            {
                spawnerController.Enabled = !spawnerController.Enabled;
                if (!spawnerController.Enabled)
                {
                    Task.Run(Collect);
                }
            }
            s = Keyboard.GetState();

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
