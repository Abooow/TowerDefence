using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Managers;

namespace TowerDefence.Views
{
    public class MapView : IView
    {
        public bool Enabled { get; set; }

        private float layerDepth;

        public MapView()
        {
            Enabled = true;
            layerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Ground);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (MapManager.LoadedMap != null)
            {
                spriteBatch.Draw(
                    MapManager.LoadedMap.GroundTexture,
                    Vector2.Zero,
                    null, 
                    Color.White, 
                    0f, 
                    Vector2.Zero, 
                    1f,
                    SpriteEffects.None, 
                    layerDepth);
            }
        }
    }
}
