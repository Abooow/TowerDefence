using MapEditor.Helpers;
using MapEditor.Managers;
using MapEditor.Models;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Controllers
{
    public class WaypointSelector : IController
    {
        public bool Enabled { get; set; }
        public Waypoint SelectedWaypoint { get; set; }

        private Camera camera;
        private MouseState mouseState;

        public WaypointSelector(Camera camera)
        {
            this.camera = camera;

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (MouseIsOverAWayPoint(out Waypoint wayPoint))
                    SelectedWaypoint = wayPoint;
                else
                    SelectedWaypoint = null;
            }
        }

        public bool MouseIsOverAWayPoint(out Waypoint wayPoint)
        {
            foreach (Waypoint point in WaypointManager.WayPoints)
            {
                if (Circle.Contains(point.Position, point.Radius, camera.ScreenToWorldPoint(mouseState.Position.ToVector2())))
                {
                    wayPoint = point;
                    return true;
                }
            }
            wayPoint = null;
            return false;
        }
    }
}
