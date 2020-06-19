using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Factories;
using TowerDefence.Managers;

namespace TowerDefence.Controllers
{
    public class EnemySpawnerController : IController
    {
        public bool Enabled { get; set; }

        private EnemyManager enemyManager;

        private float maxTimer = 20;
        private float timer;
        private Random random;

        public EnemySpawnerController(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
            random = new Random();

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0)
            {
                enemyManager.Add(EnemyFactory.GetEnemy($"Enemy{random.Next(1, 5)}"));
                timer = maxTimer;
            }
        }
    }
}
