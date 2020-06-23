using MapEditor.Helpers;
using MapEditor.Managers;
using MapEditor.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Controllers
{
    public class WaypointEditorController : IController
    {
        public bool Enabled { get; set; }

        private WaypointSelector pointSelector;
        private Camera camera;

        private Point oldMousePos;
        private bool haveSelectedPoint;

        public WaypointEditorController(Camera camera, WaypointSelector pointSelector)
        {
            this.pointSelector = pointSelector;
            this.camera = camera;

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (pointSelector.SelectedWaypoint != null)
            {
                // Move point.
                if (mouseState.LeftButton == ButtonState.Pressed && pointSelector.MouseIsOverAWayPoint(out _))
                {
                    haveSelectedPoint = true;
                    pointSelector.Enabled = false;
                }
                if (haveSelectedPoint && mouseState.LeftButton == ButtonState.Released)
                {
                    pointSelector.Enabled = true;
                    haveSelectedPoint = false;
                }
                if (haveSelectedPoint) pointSelector.SelectedWaypoint.Position -= (oldMousePos - mouseState.Position).ToVector2() / camera.Scale.X;

                // Delete point.
                if (Keyboard.GetState().IsKeyDown(Keys.Delete)) WaypointManager.WayPoints.Remove(pointSelector.SelectedWaypoint);
                
                // Deselect point.
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Delete)) pointSelector.SelectedWaypoint = null;
            }
            else pointSelector.Enabled = true;

            oldMousePos = mouseState.Position;
        }
    }
}
