using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Moldels
{
    public class Map
    {
        public Texture2D GroundTexture { get; }
        public Bitmap PermittedTowerPlacementTexture { get; }
        public Vector2[] EnemyPath { get; }

        public Map(Texture2D groundTexture, Bitmap permittedTowerPlacementTexture, Vector2[] enemyPath)
        {
            GroundTexture = groundTexture;
            PermittedTowerPlacementTexture = permittedTowerPlacementTexture;
            EnemyPath = enemyPath;
        }
    }
}
