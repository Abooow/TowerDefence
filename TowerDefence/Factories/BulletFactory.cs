using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Moldels;

namespace TowerDefence.Factories
{
    public static class BulletFactory
    {
        public static Dictionary<string, Bullet> Bullets { get; private set; } = new Dictionary<string, Bullet>();
        public static Bullet DefaultBullet { get; set; }

        public static void Add(string name, Bullet bullet)
        {
            if (!Bullets.ContainsKey(name))
            {
                Bullets.Add(name, bullet);
                if (DefaultBullet == null) DefaultBullet = bullet;
            }
        }

        public static Bullet GetBullet(string name)
        {
            Bullet bulletToCopy = DefaultBullet;

            if (Bullets.ContainsKey(name)) bulletToCopy = Bullets[name];

            return bulletToCopy == null ? null : new Bullet(bulletToCopy.Sprite, bulletToCopy.MaxDistance, bulletToCopy.Speed, bulletToCopy.Damage, bulletToCopy.LayerDepth);
        }
    }
}
