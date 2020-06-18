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
        public bool Enabled { get; set; }

        private SelectTowerController selectTowerController;

        public SelectedTowerView(SelectTowerController selectTowerController)
        {
            this.selectTowerController = selectTowerController;
            Enabled = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw selected tower.
            if (selectTowerController.SelectedTower != null)
            {
                // Draw range area.
                spriteBatch.Draw(
                    selectTowerController.SelectedTower.RangeTexture,
                    selectTowerController.SelectedTower.Position - new Vector2(selectTowerController.SelectedTower.RangeRadius),
                    null,
                    Color.White * 0.4f,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    selectTowerController.SelectedTower.BaseLayerDepth - 0.001f);

                selectTowerController.SelectedTower.Draw(spriteBatch, Color.White);
            }

        }
    }
}
