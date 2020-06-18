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

        public Map(Texture2D groundTexture, Bitmap permittedTowerPlacementTexture)
        {
            GroundTexture = groundTexture;
            PermittedTowerPlacementTexture = permittedTowerPlacementTexture;
        }

    }
}
