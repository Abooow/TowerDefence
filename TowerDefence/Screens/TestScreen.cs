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
            controllers.Add(new TestTowerPlacerScript(towerPlacer, bulletManager)); // TEST!

            // Views.
            views.Add(new SelectedTowerView(selectTowerController));
            views.Add(new MapView());
            views.Add(enemyManager);
            views.Add(towerManager);
            views.Add(bulletManager);
                spawnerController.Enabled = false;

            a = new SamplerState()
            {
                FilterMode = TextureFilterMode.Default,
                Filter = TextureFilter.Point,
            };
        }

        KeyboardState s;
        public override void Update(float deltaTime)
        {
            towerManager.Update(deltaTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && s.IsKeyUp(Keys.Enter))
                spawnerController.Enabled = !spawnerController.Enabled;
            s = Keyboard.GetState();

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
            else if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (towerPlacer.TargetTower != null) towerPlacer.TargetTower = null;
            }

            base.Update(deltaTime);
        }

        SamplerState a;
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, camera.GetTranslationMatrix());

            // Draw tower to place.
            towerPlacer.Draw(spriteBatch);

            foreach (IView view in views)
                if (view.Enabled) view.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        private void OnEnemyReachedGoal(Enemy enemy)
        {
            enemy.RemoveFromWorld();
        }
    }
}
