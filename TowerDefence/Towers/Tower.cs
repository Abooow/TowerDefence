using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.Towers
{
    public abstract class Tower
    {
        public float Range { get; set; }
        public Rectangle Size { get; set; }
        public Texture2D Texture { get; set; }

        public Tower(float range, Rectangle size, Texture2D texture)
        {
            Range = range;
            Size = size;
            Texture = texture;
        }

        public abstract void Update(float deltaTime);
        public abstract void Draw(float deltaTime);
    }
}
