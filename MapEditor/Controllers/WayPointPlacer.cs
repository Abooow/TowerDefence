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
    public class WayPointPlacer : IController
    {
        public bool Enabled { get; set; }

        private Camera camera;
        private WayPointSelector pointSelector;
        private MouseState lastMouse;

        public WayPointPlacer(Camera camera, WayPointSelector pointSelector)
        {
            this.camera = camera;
            this.pointSelector = pointSelector;
            lastMouse = Mouse.GetState();

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            MouseState mouseState = Mouse.GetState();

            if ((mouseState.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released) && !pointSelector.MouseIsOverAWayPoint(out _))
                WayPointManager.AddPoint(camera.ScreenToWorldPoint(mouseState.Position.ToVector2()));

            lastMouse = mouseState;
        }
    }
}
