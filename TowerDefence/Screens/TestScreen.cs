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
    public class TestScreen : BaseScreen
    {
        private Camera camera;
        private EnemyManager enemyManager;
        private TowerPlacer towerPlacer;
        private TowerManager towerManager;
        private SelectTowerController selectTowerController;
        private RenderTarget2D renderTarget;

        public TestScreen()
        {
            camera = new Camera(new Vector2(800, 480))
            {
                Scale = new Vector2(0.5f)
            };

            enemyManager = new EnemyManager();
            towerManager = new TowerManager();
            towerPlacer = new TowerPlacer(towerManager);

            enemyManager.OnEnemyReachedLastPoint += OnEnemyReachedGoal;

            // Controllers.
            controllers.Add(selectTowerController = new SelectTowerController(towerManager, towerPlacer, camera));
            controllers.Add(new MapMoverController(camera));
            controllers.Add(new EnemySpawnerController(enemyManager));
            controllers.Add(enemyManager);

            // Views.
            views.Add(new SelectedTowerView(selectTowerController));
            views.Add(new MapView());
            views.Add(enemyManager);

            renderTarget = new RenderTarget2D(Game1.Graphics, 800, 480);
        }

        public override void Update(float deltaTime)
        {
            towerManager.Update(deltaTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                selectTowerController.SelectedTower = null;
                towerPlacer.TargetTower = new TestTower();
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
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, camera.GetTranslationMatrix());

            // Draw tower to place.
            towerPlacer.Draw(spriteBatch);

            // Draw all existing towers.
            foreach (BaseTower tower in towerManager.Towers)
            {
                if (!(tower == selectTowerController.SelectedTower)) tower.Draw(spriteBatch, Color.White);
            }

            foreach (IView view in views)
                if (view.Enabled) view.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        private void OnEnemyReachedGoal(Enemy enemy)
        {
            enemyManager.Enemies.Remove(enemy);
        }
    }
}
