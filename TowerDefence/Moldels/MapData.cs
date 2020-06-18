using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Moldels
{
    public struct MapData
    {
        public string FilePath;
        public string GroundTexturePath;
        public string PermittedTowerPlacementTexturePath;

        public MapData(string filePath, string groundTexturePath, string permittedTowerPlacementTexturePath)
        {
            FilePath = filePath;
            GroundTexturePath = groundTexturePath;
            PermittedTowerPlacementTexturePath = permittedTowerPlacementTexturePath;
        }
    }
}
