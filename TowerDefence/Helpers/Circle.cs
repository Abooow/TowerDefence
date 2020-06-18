using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Helpers
{
    public static class Circle
    {
        public static bool Intercects(Vector2 circle1Pos, float circle1circleRadius, Vector2 circle2Pos, float circle2circleRadius)
        {
            return (circle2Pos - circle1Pos).LengthSquared() < Math.Pow(circle1circleRadius + circle2circleRadius, 2);
        }

        public static bool Intercects(Vector2 circlePos, float circleRadius, Rectangle rectangle)
        {
            // temporarectangle.Y variables to set edges for testing
            float testX = circlePos.X;
            float testY = circlePos.Y;

            // which edge is closest?
            if (circlePos.X < rectangle.X) testX = rectangle.X;      // test left edge
            else if (circlePos.X > rectangle.X + rectangle.Width) testX = rectangle.X + rectangle.Width;   // right edge
            if (circlePos.Y < rectangle.Y) testY = rectangle.Y;      // top edge
            else if (circlePos.Y > rectangle.Y + rectangle.Height) testY = rectangle.Y + rectangle.Height;   // bottom edge

            // get distance from closest edges
            float distX = circlePos.X - testX;
            float distY = circlePos.Y - testY;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the circleRadius, collision!
            return distance <= circleRadius;
        }

        public static bool Contains(Vector2 circlePos, float circleRadius, Vector2 point)
        {
            return (point - circlePos).LengthSquared() < Math.Pow(circleRadius + 6, 2);
        }

        public static Texture2D GetTexture(GraphicsDevice graphicsDevice, int diameter)
        {
            Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
            Color[] colorData = new Color[diameter * diameter];

            float radius = diameter / 2f;
            float radiusSq = radius * radius;

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    int index = x * diameter + y;
                    Vector2 pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= radiusSq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }
            texture.SetData(colorData);

            return texture;
        }
    }
}
