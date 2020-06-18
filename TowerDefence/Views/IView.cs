using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.Views
{
    public interface IView
    {
        bool Enabled { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
