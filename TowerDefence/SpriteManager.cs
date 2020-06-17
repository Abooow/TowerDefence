using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence
{
    public static class SpriteManager
    {
        private static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();

        public static void Add(string name, Texture2D sprite)
        {
            if (!sprites.ContainsKey(name)) sprites.Add(name, sprite);
            else sprites[name] = sprite;
        }

        public static Texture2D Get(string name)
        {
            if (sprites.TryGetValue(name, out Texture2D sprite)) return sprite;
            return null;
        }
    }
}
