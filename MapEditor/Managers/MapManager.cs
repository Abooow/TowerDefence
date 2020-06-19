using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Managers
{
    public static class MapManager
    {
        public static Texture2D LoadedMapTexture { get; private set; }
        public static Texture2D LoadedPermittedTowerPlacementTexture { get; private set; }
        public static string LoadedMapTexturePath { get; private set; }
        public static string LoadedPermittedTowerPlacementTexturePath { get; private set; }

        public static void Load(GraphicsDevice graphics, string mapTexturePath, string permittedTowerPlacementTexture)
        {
            LoadedMapTexture = LoadTexture(graphics, mapTexturePath);
            LoadedPermittedTowerPlacementTexture = LoadTexture(graphics, permittedTowerPlacementTexture);
            LoadedMapTexturePath = mapTexturePath;
            LoadedPermittedTowerPlacementTexturePath = permittedTowerPlacementTexture;
        }

        private static Texture2D LoadTexture(GraphicsDevice graphics, string path)
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
    }
}
