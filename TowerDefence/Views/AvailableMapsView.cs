using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;

namespace TowerDefence.Views
{
    public class AvailableMapsView : IView
    {
        public HashSet<int> ViewGroupId { get; }
        public bool Enabled { get; set; }

        public float XOffset { get; set; }
        public float YOffset { get; set; }
        public Point Margin { get; }
        public Point ThumbnailSize { get; }
        public int MinScrollValue { get; }
        public int MaxScrollValue { get; }

        private int roundedMapNumbers;

        public AvailableMapsView()
        {
            ThumbnailSize = new Point(280, 150);
            Margin = new Point(50, 50);

            XOffset = Game1.ScreenSize.X * 0.5f - (ThumbnailSize.X * 3f + Margin.X * 2f) * 0.5f;
            YOffset = Game1.ScreenSize.Y * 0.5f - (ThumbnailSize.Y * 2f + Margin.Y * 1f) * 0.5f;

            roundedMapNumbers = (MapManager.TotalLoadedMaps / 7 + 1) * 6;

            MinScrollValue = (int)XOffset;
            MaxScrollValue = ((MapManager.TotalLoadedMaps / 7) * 6) / 2 * -(ThumbnailSize.X + Margin.X) + MinScrollValue;

            ViewGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < roundedMapNumbers; i++)
            {
                Point position = new Point(
                    (int)XOffset + (i / 2) * (ThumbnailSize.X + Margin.X), 
                    (int)YOffset + (i % 2) * (ThumbnailSize.Y + Margin.Y));

                spriteBatch.Draw(
                        AssetManager.GetTexture($"Pixel"),
                        new Rectangle(position, ThumbnailSize),
                        null,
                        Color.Black * 0.8f,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0.4f);

                if (i < MapManager.TotalLoadedMaps)
                {
                    Texture2D thumbnail = AssetManager.GetTexture($"Thumbnail{i}");
                    Vector2 newSize = new Vector2();
                    if (thumbnail.Width >= thumbnail.Height) newSize = new Vector2((ThumbnailSize.Y / (float)thumbnail.Height) * thumbnail.Width, ThumbnailSize.Y);
                    else newSize = new Vector2(ThumbnailSize.X, (ThumbnailSize.X / (float)thumbnail.Width) * thumbnail.Height);

                    spriteBatch.Draw(
                        thumbnail,
                        new Rectangle(position + (ThumbnailSize.ToVector2() * 0.5f).ToPoint(), newSize.ToPoint()),
                        null,
                        Color.White,
                        0f, 
                        thumbnail.Bounds.Size.ToVector2() * 0.5f,
                        SpriteEffects.None,
                        0.5f);

                    string name = System.IO.Path.GetFileNameWithoutExtension(MapManager.Maps[i].FilePath);
                    spriteBatch.DrawString(
                        AssetManager.GetFont("BaseFont"),
                        name,
                        position.ToVector2() + new Vector2(ThumbnailSize.X * 0.5f, ThumbnailSize.Y + 5f),
                        Color.Black,
                        0f,
                        AssetManager.GetFont("BaseFont").MeasureString(name) * new Vector2(0.5f, 0),
                        1f,
                        SpriteEffects.None,
                        0.5f);
                }
            }
        }
    }
}
