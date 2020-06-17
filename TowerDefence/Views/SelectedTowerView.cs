using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Controllers;

namespace TowerDefence.Views
{
    public class SelectedTowerView : IView
    {
        private SelectTowerController selectTowerController;

        public SelectedTowerView(SelectTowerController selectTowerController)
        {
            this.selectTowerController = selectTowerController;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw selected tower.
            if (selectTowerController.SelectedTower != null)
            {
                // Draw range area.
                spriteBatch.Draw(
                    selectTowerController.SelectedTower.RangeTexture,
                    selectTowerController.SelectedTower.Position - new Vector2(selectTowerController.SelectedTower.RangeRadius) * 0.5f,
                    Color.White * 0.3f);

                selectTowerController.SelectedTower.Draw(spriteBatch, Color.White);
            }

        }
    }
}
