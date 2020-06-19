using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Views
{
    public interface IView
    {
        bool Enabled { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
