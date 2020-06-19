using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Moldels
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }
        public Vector2 Origin { get; set; }

        public Sprite(Texture2D texture, Rectangle sourceRect, Vector2 origin)
        {
            Texture = texture;
            SourceRect = sourceRect;
            Origin = origin;
        }

        public Sprite(Texture2D texture, Rectangle sourceRect)
            : this(texture, sourceRect, Vector2.Zero)
        {
        }

        public Sprite(Texture2D texture)
            : this(texture, texture.Bounds)
        {
        }

        public static implicit operator Texture2D(Sprite sprite)
        {
            return sprite.Texture;
        }
    }
}
