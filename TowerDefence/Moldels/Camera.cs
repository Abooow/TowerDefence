using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Moldels
{
    /// <summary>
    /// A camera like object that determines what is displayed on the screen.
    /// </summary>
    public class Camera
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 ViewSize { get; set; }

        /// <summary>
        /// Creates a new instance of the Camera class.
        /// </summary>
        /// <param name="viewSize">The view size of this Camera.</param>
        public Camera(Vector2 viewSize)
        {
            ViewSize = viewSize;
            Scale = Vector2.One;
        }

        /// <summary>
        /// Get the area that this Camera sees.
        /// </summary>
        public Rectangle View => new Rectangle((Position / Scale).ToPoint(), ViewSize.ToPoint());

        /// <summary>
        /// Get the translation matrix of this camera, containing information about the position, rotation and scale.
        /// </summary>
        /// <returns>A matrix that contains information about the position, rotation and scale of this camera.</returns>
        public Matrix GetTranslationMatrix()
        {
            Vector3 position = new Vector3(-Position.X, -Position.Y, 0);
            Vector3 zoom = new Vector3(Scale.X, Scale.Y, 0);

            return Matrix.CreateTranslation(position) * Matrix.CreateScale(zoom);
        }

        /// <summary>
        /// Zoom the Camera a given amount towards a given position.
        /// </summary>
        /// <param name="amount">How much to zoom.</param>
        /// <param name="towardsScreenPoint">The position to zoom towards in screen space.</param>
        public void Zoom(float amount, Vector2 towardsScreenPoint)
        {
            Vector2 before = ScreenToWorldPoint(towardsScreenPoint);
            Scale *= amount;
            Vector2 after = ScreenToWorldPoint(towardsScreenPoint);

            Position += before - after;
        }

        /// <summary>
        /// Convert a point on the screen (camera) to a point in the world.
        /// </summary>
        /// <param name="point">The camera point to convert.</param>
        /// <returns>The world point of the given screen point.</returns>
        public Vector2 ScreenToWorldPoint(Vector2 point)
        {
            return point / Scale + Position;
        }

        /// <summary>
        /// Convert a point in the world to a point on the screen (camera).
        /// </summary>
        /// <param name="point">The world point to convert.</param>
        /// <returns>The screen point of the given world point.</returns>
        public Vector2 WorldToScreenPoint(Vector2 point)
        {
            return point * Scale - Position;
        }
    }
}
