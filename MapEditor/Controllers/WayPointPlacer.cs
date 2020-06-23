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
    public class WaypointPlacer : IController
    {
        public bool Enabled { get; set; }

        private Camera camera;
        private WaypointSelector pointSelector;
        private MouseState lastMouse;

        public WaypointPlacer(Camera camera, WaypointSelector pointSelector)
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
                WaypointManager.AddPoint(camera.ScreenToWorldPoint(mouseState.Position.ToVector2()));

            lastMouse = mouseState;
        }
    }
}
