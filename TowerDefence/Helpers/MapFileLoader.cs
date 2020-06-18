using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Moldels;

namespace TowerDefence.Helpers
{
    public static class MapFileLoader
    {
        public static string MapsFolderPath = "../../../../Maps";

        public static MapData[] FindAllMapFiles(string pattern)
        {
            string[] files = Directory.GetFiles(MapsFolderPath, pattern, SearchOption.AllDirectories);

            List<MapData> maps = new List<MapData>();
            foreach (string filePath in files)
            {
                (string, string) mapData = GetMapData(filePath);
                maps.Add(new MapData(filePath, mapData.Item1, mapData.Item2));
            }

            return maps.ToArray();
        }

        private static (string GroundTexturePath, string PermittedTowerPlacementTexturePath) GetMapData(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string GroundTexturePath = "";
            string PermittedTowerPlacementTexturePath = "";

            string GetValueStr(string line, string key)
            {
                if (line.Trim().ToLower().StartsWith(key.ToLower()))
                {
                    string[] valueString = line.Split(new char[] { '=', '\n', '\r' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (valueString.Length > 1) return valueString[1].Trim();
                }

                return "";
            }

            foreach (string line in lines)
            {
                string groundTexturePath = GetValueStr(line, "GroundTexturePath");
                string permittedTowerPlacementTexturePath = GetValueStr(line, "PermittedTowerPlacementTexturePath");

                if (groundTexturePath != "") GroundTexturePath = $"{Directory.GetParent(path)}\\{groundTexturePath}";
                if (permittedTowerPlacementTexturePath != "") PermittedTowerPlacementTexturePath = $"{Directory.GetParent(path)}\\{permittedTowerPlacementTexturePath}";
            }

            return (GroundTexturePath, PermittedTowerPlacementTexturePath);
        }
    }
}
