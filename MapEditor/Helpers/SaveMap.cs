using MapEditor.Managers;
using MapEditor.Models;
using MapEditor.Moldels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapEditor.Helpers
{
    public static class SaveMap
    {
        public static void Save(string path)
        {
            Vector2[] GetWayPointPositions()
            {
                Vector2[] positions = new Vector2[WayPointManager.WayPoints.Count];
                for (int i = 0; i < WayPointManager.WayPoints.Count; i++)
                    positions[i] = WayPointManager.WayPoints[i].Position;
                return positions;
            }

            MapData mapData = new MapData(path, MapManager.LoadedMapTexturePath, MapManager.LoadedPermittedTowerPlacementTexturePath, GetWayPointPositions());

            XmlSerializer serializer = new XmlSerializer(typeof(MapData));
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, mapData);
            }
        }
    }
}
