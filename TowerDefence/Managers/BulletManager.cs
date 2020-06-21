using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Controllers;
using TowerDefence.Moldels;
using TowerDefence.Views;

namespace TowerDefence.Managers
{
    public class BulletManager : IController, IView
    {
        public bool Enabled { get; set; }

        private List<Bullet> bullets;
        private EnemyManager enemyManager;

        public BulletManager(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;

            bullets = new List<Bullet>();

            Enabled = true;
        }

        public void SpawnBullet(Bullet bullet, Vector2 position, Vector2 direction)
        {
            bullet.Position = position;
            bullet.Direction = direction;
            bullets.Add(bullet);
        }

        public void Update(float deltaTime)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (bullets[i].Update(deltaTime, enemyManager.Query(bullets[i].Position)))
                    bullets.Remove(bullets[i]);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}
