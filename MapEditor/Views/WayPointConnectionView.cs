using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapEditor.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Views
{
    public class WayPointConnectionView : IView
    {
        public bool Enabled { get; set; }

        private float lineLayerDepth;
        private Texture2D pixel;

        public WayPointConnectionView()
        {
            lineLayerDepth = SortingOrder.GetLayerDepth(-5, SortingLayer.WayPoints);
            pixel = new Texture2D(Game1.Graphics, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            Enabled = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < WayPointManager.WayPoints.Count - 1; i++)
            {
                DrawLine(spriteBatch, WayPointManager.WayPoints[i].Position, WayPointManager.WayPoints[i + 1].Position, Color.Black * 0.9f, 3f);
            }
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(pixel, point, null, color, angle, origin, scale, SpriteEffects.None, lineLayerDepth);
        }
    }
}
