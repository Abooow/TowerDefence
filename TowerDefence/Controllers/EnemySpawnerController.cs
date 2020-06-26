using Microsoft.Xna.Framework;
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

        private float maxTimer = 0.0f;
        private float timer;
        private Random random;

        public EnemySpawnerController(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
            random = new Random();

            Enabled = true;
        }

        int tot;
        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0f)
            {
                enemyManager.Spawn(EnemyFactory.GetEnemy($"Enemy3"));
                timer = maxTimer;
                tot++;
            }
        }
    }
}
