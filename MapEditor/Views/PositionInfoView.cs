using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapEditor.Controllers;
using MapEditor.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapEditor.Views
{
    public class PositionInfoView : IUIView
    {
        public static SpriteFont Font;

        public bool Enabled { get; set; }

        private Camera camera;
        private WayPointSelector pointSelector;

        public PositionInfoView(Camera camera, WayPointSelector pointSelector)
        {
            this.camera = camera;
            this.pointSelector = pointSelector;

            Font = Game1.ContentManager.Load<SpriteFont>("Font");

            Enabled = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string mousePosStr = $"Mouse: {camera.ScreenToWorldPoint(Mouse.GetState().Position.ToVector2())}";
            spriteBatch.DrawString(Font, mousePosStr, new Vector2(800, 0), Color.Black, 0f, Font.MeasureString(mousePosStr) * new Vector2(1f, 0f), 1f, SpriteEffects.None, 1f);

            if (pointSelector.SelectedWayPoint != null)
            {
                string selectedPointPosStr = $"Selected point: {pointSelector.SelectedWayPoint.Position}";
                spriteBatch.DrawString(Font, selectedPointPosStr, new Vector2(800, 25), Color.Black, 0f, Font.MeasureString(selectedPointPosStr) * new Vector2(1f, 0f), 1f, SpriteEffects.None, 1f);
            }
        }
    }
}
