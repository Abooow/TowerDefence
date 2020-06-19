using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Controllers;
using TowerDefence.Moldels;
using TowerDefence.Views;

namespace TowerDefence.Managers
{
    public class EnemyManager : IController, IView
    {
        public delegate void EnemyEvent(Enemy enemy);

        public bool Enabled { get; set; }
        public List<Enemy> Enemies { get; }

        public EnemyEvent OnEnemyReachedLastPoint;

        public EnemyManager()
        {
            Enemies = new List<Enemy>();

            Enabled = true;
        }

        public void Add(Enemy enemy)
        {
            if (enemy != null && !Enemies.Contains(enemy)) Enemies.Add(enemy); 
        }

        public void Update(float deltaTime)
        {
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(deltaTime);
                if (Enemies[i].HaveReachedLastWayPoint) OnEnemyReachedLastPoint?.Invoke(Enemies[i]);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
