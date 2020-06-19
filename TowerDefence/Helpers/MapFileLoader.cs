using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TowerDefence.Moldels;

namespace TowerDefence.Helpers
{
    public static class MapFileLoader
    {
        public static string MapsFolderPath = "../../../../Maps";

        public static MapData[] FindAllMapFiles()
        {
            string[] files = Directory.GetFiles(MapsFolderPath, "*.xml", SearchOption.AllDirectories);

            List<MapData> maps = new List<MapData>();
            foreach (string filePath in files)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MapData));
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    MapData? map = serializer.Deserialize(fs) as MapData?;
                    if (map != null) maps.Add((MapData)map);
                }
            }

            return maps.ToArray();
        }

        [Obsolete]
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
