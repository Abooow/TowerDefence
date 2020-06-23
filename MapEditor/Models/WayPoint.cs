using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Models
{
    public class Waypoint
    {
        public Vector2 Position;
        public float Radius;
        public Texture2D Texture;

        public Waypoint(Vector2 position, float radius, Texture2D texture)
        {
            Position = position;
            Radius = radius;
            Texture = texture;
        }
    }
}
