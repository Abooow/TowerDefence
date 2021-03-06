﻿using Microsoft.Xna.Framework;
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
            Texture2D groundTexture = null; 
            Bitmap permittedTowerPlaceTexture = null;
            if (mapData.GroundTexturePath != "") groundTexture = AssetManager.LoadTexture2D(graphics, mapData.GroundTexturePath);
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
