using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.Towers
{
    public class TestTower : Tower
    {
        public TestTower()
            : base(10f, new Rectangle(), SpriteManager.Get("test"))
        {
        }

        public override void Update(float deltaTime)
        {
        }

        public override void Draw(float deltaTime)
        {
        }
    }
}
