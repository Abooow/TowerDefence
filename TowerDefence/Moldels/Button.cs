using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;

namespace TowerDefence.Moldels
{
    public class Button
    {
        public EventHandler OnClicked;

        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public string Text { get; set; }
        public object AttachedObject { get; set; }
        public float LayerDepth { get; set; }

        public Rectangle Bounds => new Rectangle(Position.ToPoint(), Size.ToPoint());

        public Button(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;

            Visible = true;
            Enabled = true;
            Color = new Color(51, 51, 51);
        }

        public void Update(MouseState mouse, bool clicked)
        {
            Rectangle newBounds = new Rectangle((Position + Origin).ToPoint(), Size.ToPoint());
            if (Enabled && newBounds.Contains(mouse.Position))
            {
                if (clicked)
                {
                    OnClicked?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled && Visible)
            {
                spriteBatch.Draw(
                    AssetManager.GetTexture("Pixel"),
                    Bounds,
                    null,
                    Color,
                    0f,
                    Origin,
                    SpriteEffects.None,
                    LayerDepth);
            }
        }
    }
}
