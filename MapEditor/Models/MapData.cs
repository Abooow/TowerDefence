﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapEditor.Moldels
{
    [Serializable]
    [XmlRoot("Map")]
    public struct MapData
    {
        [XmlIgnore]
        public string FilePath;

        public string GroundTexturePath;
        public string PermittedTowerPlacementTexturePath;
        public Vector2[] EnemyPath;

        public MapData(string filePath, string groundTexturePath, string permittedTowerPlacementTexturePath, Vector2[] enemyPath)
        {
            FilePath = filePath;
            GroundTexturePath = groundTexturePath;
            PermittedTowerPlacementTexturePath = permittedTowerPlacementTexturePath;
            EnemyPath = enemyPath;
        }
    }
}
