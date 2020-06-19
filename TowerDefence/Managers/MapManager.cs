using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Managers
{
    public static class MapManager
    {
        public static MapData[] Maps { get; private set; }
        public static Map LoadedMap { get; private set; } 

        public static int TotalLoadedMaps => Maps.Length;

        public static void LoadAllMaps()
        {
            Maps = MapFileLoader.FindAllMapFiles();
        }

        public static Map CreateMap(MapData mapData, GraphicsDevice graphics)
        {
            Texture2D LoadTexture(string path)
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

            Texture2D groundTexture = null; 
            Bitmap permittedTowerPlaceTexture = null;
            if (mapData.GroundTexturePath != "") groundTexture = LoadTexture(mapData.GroundTexturePath);
            if (mapData.PermittedTowerPlacementTexturePath != "") permittedTowerPlaceTexture = new Bitmap(mapData.PermittedTowerPlacementTexturePath);

            return new Map(groundTexture, permittedTowerPlaceTexture, mapData.EnemyPath);
        }

        public static bool LoadMap(Map map)
        {
            if (map == null || map.PermittedTowerPlacementTexture == null) return false;

            LoadedMap = map;
            return true;
        }
    }
}
