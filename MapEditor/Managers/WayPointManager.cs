using MapEditor.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Managers
{
    public static class WayPointManager
    {
        public static float DefaultWayPointRadius;
        public static Texture2D DefaultWayPointTexture;
        public static List<WayPoint> WayPoints { get; private set; } = new List<WayPoint>();

        public static void AddPoint(Vector2 position)
        {
            bool ExistPosition()
            {
                foreach (var point in WayPoints)
                    if (point.Position == position) return true;
                return false;
            }

            if (!ExistPosition()) WayPoints.Add(new WayPoint(position, DefaultWayPointRadius, DefaultWayPointTexture));
        }
    }
}
