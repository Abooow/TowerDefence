using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Controllers;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers;
using TowerDefence.Views;

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
            camera = new Camera(new Vector2(800, 480))
            {
                Scale = new Vector2(0.5f)
            };

            enemyManager = new EnemyManager();
            towerManager = new TowerManager(enemyManager);
            bulletManager = new BulletManager(enemyManager);
            towerPlacer = new TowerPlacer(towerManager);
            selectTowerController = new SelectTowerController(towerManager, towerPlacer, camera);

            enemyManager.OnEnemyReachedLastPoint += OnEnemyReachedGoal;

            // Controllers.
            controllers.Add(selectTowerController);
            controllers.Add(new MapMoverController(camera));
            controllers.Add(spawnerController = new EnemySpawnerController(enemyManager));
            controllers.Add(enemyManager);
            controllers.Add(towerManager);
            controllers.Add(bulletManager);

            // Views.
            views.Add(new SelectedTowerView(selectTowerController));
            views.Add(new MapView());
            views.Add(enemyManager);
            views.Add(towerManager);
            views.Add(bulletManager);
        }

        public override void Update(float deltaTime)
        {
            towerManager.Update(deltaTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                spawnerController.Enabled = true;
            else
                spawnerController.Enabled = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                selectTowerController.SelectedTower = null;
                towerPlacer.TargetTower = new TestTower(bulletManager);
            }

            if (towerPlacer.HaveTargetTower) towerPlacer.MoveTower(camera.ScreenToWorldPoint(Mouse.GetState().Position.ToVector2()));

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (towerPlacer.TargetTower != null)
                    towerPlacer.PlaceTower();
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointWrap, null, null, null, camera.GetTranslationMatrix());

            // Draw tower to place.
            towerPlacer.Draw(spriteBatch);

            foreach (IView view in views)
                if (view.Enabled) view.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        private void OnEnemyReachedGoal(Enemy enemy)
        {
            enemyManager.Remove(enemy);
        }
    }
}
