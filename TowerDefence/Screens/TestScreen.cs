using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Controllers;
using TowerDefence.Helpers;
using TowerDefence.Managers;
using TowerDefence.Towers;
using TowerDefence.Views;

namespace TowerDefence.Screens
{
    public class TestScreen : BaseScreen
    {
        private TowerPlacer towerPlacer;
        private TowerManager towerManager;
        private SelectTowerController selectTowerController;

        public TestScreen()
        {
            towerManager = new TowerManager();
            towerPlacer = new TowerPlacer(towerManager);

            controllers.Add(selectTowerController = new SelectTowerController(towerManager, towerPlacer));
            views.Add(new SelectedTowerView(selectTowerController));
        }

        public override void Update(float deltaTime)
        {
            towerManager.Update(deltaTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                towerPlacer.TargetTower = new TestTower();
            }

            if (towerPlacer.HaveTargetTower) towerPlacer.MoveTower(Mouse.GetState().Position.ToVector2());

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (towerPlacer.TargetTower != null)
                    towerPlacer.PlaceTower();
            }

            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            // Draw tower to place.
            towerPlacer.Draw(spriteBatch);

            // Draw all existing towers.
            foreach (BaseTower tower in towerManager.Towers)
            {
                if (!(tower == selectTowerController.SelectedTower)) tower.Draw(spriteBatch, Color.White);
            }

            foreach (IView view in views)
                view.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
