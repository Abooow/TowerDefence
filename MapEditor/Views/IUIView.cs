using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Views
{
    public interface IUIView
    {
        bool Enabled { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
