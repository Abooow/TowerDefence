using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;

namespace TowerDefence.Helpers
{
    public static class Line
    {
        private static Texture2D pixel;
        private static Vector2 origin;

        static Line()
        {
            pixel = AssetManager.GetTexture("Pixel");
            origin = new Vector2(0f, 0.5f);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f, float layerDepth = 1f)
        {
            float distance = Vector2.Distance(point1, point2);
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness, layerDepth);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f, float layerDepth = 1f)
        {
            Vector2 scale = new Vector2(length, thickness);

            spriteBatch.Draw(pixel, point, null, color, angle, origin, scale, SpriteEffects.None, layerDepth);
        }
    }
}
