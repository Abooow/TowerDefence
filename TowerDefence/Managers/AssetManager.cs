using System.Collections.Generic;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Moldels;

namespace TowerDefence.Managers
{
    public static class AssetManager
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        public static Texture2D LoadTexture2D(GraphicsDevice graphics, string path)
        {
            using (Image image = Image.FromFile(path))
            {
                int w = image.Width;
                int h = image.Height;
                Bitmap bitmap = new Bitmap(image);
                uint[] data = new uint[w * h];
                for (int i = 0; i != bitmap.Width; ++i)
                {
                    for (int j = 0; j != bitmap.Height; ++j)
                    {
                        System.Drawing.Color pixel = bitmap.GetPixel(i, j);
                        Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.FromNonPremultiplied(pixel.R, pixel.G, pixel.B, pixel.A);
                        data[i + j * w] = color.PackedValue;
                    }
                }
                Texture2D _texture = new Texture2D(graphics, w, h);
                _texture.SetData(data);

                return _texture;
            }
        }

        public static void AddTexture(string name, Texture2D texture)
        {
            if (!textures.ContainsKey(name)) textures.Add(name, texture);
            else textures[name] = texture;
        }

        public static Texture2D GetTexture(string name)
        {
            if (textures.TryGetValue(name, out Texture2D sprite)) return sprite;
            return null;
        }

        public static void AddFont(string name, SpriteFont font)
        {
            if (!fonts.ContainsKey(name)) fonts.Add(name, font);
            else fonts[name] = font;
        }

        public static SpriteFont GetFont(string name)
        {
            if (fonts.TryGetValue(name, out SpriteFont font)) return font;
            return null;
        }

        public static void AddSprite(string name, Sprite sprite)
        {
            if (!sprites.ContainsKey(name)) sprites.Add(name, sprite);
            else sprites[name] = sprite;
        }

        public static Sprite GetSprite(string name)
        {
            if (sprites.TryGetValue(name, out Sprite sprite)) return sprite;
            return null;
        }
    }
}
