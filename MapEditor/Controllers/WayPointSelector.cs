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
    public class WayPointSelector : IController
    {
        public bool Enabled { get; set; }
        public WayPoint SelectedWayPoint { get; set; }

        private Camera camera;
        private MouseState mouseState;

        public WayPointSelector(Camera camera)
        {
            this.camera = camera;

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (MouseIsOverAWayPoint(out WayPoint wayPoint))
                    SelectedWayPoint = wayPoint;
                else
                    SelectedWayPoint = null;
            }
        }

        public bool MouseIsOverAWayPoint(out WayPoint wayPoint)
        {
            foreach (WayPoint point in WayPointManager.WayPoints)
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
