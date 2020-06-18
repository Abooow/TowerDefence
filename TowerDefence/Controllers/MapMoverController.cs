using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Controllers
{
    public class MapMoverController : IController
    {
        public bool Enabled { get; set; }

        private Camera camera;
        private Vector2 minPos;
        private Vector2 maxPos;
        private Vector2 minZoom;
        private Vector2 maxZoom;

        private Point oldMousePos;
        private float oldScrollValue;

        public MapMoverController(Camera camera)
        {
            this.camera = camera;

            maxPos = -MapManager.LoadedMap.GroundTexture.Bounds.Size.ToVector2();
            minPos = new Vector2(0f);
            minZoom = new Vector2(0.2f);
            maxZoom = new Vector2(3f);

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            MouseState mouseState = Mouse.GetState();

            // Move.
            if (mouseState.RightButton == ButtonState.Pressed || mouseState.MiddleButton == ButtonState.Pressed)
            {
                Vector2 newPos = camera.Position + (oldMousePos - mouseState.Position).ToVector2() /  camera.Scale.X;
                camera.Position = newPos;
            }
            oldMousePos = mouseState.Position;

            // Zoom.
            if (mouseState.ScrollWheelValue != oldScrollValue)
            {
                float deltaScroll = mouseState.ScrollWheelValue - oldScrollValue;
                camera.Zoom(deltaScroll <= 0f ? 0.9f : 1.1f, camera.ScreenToWorldPoint(mouseState.Position.ToVector2()));
                //camera.Position = Vector2.Clamp(camera.Position, minPos, maxPos);
            }
            oldScrollValue = mouseState.ScrollWheelValue;
        }
    }
}
