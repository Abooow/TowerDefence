using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Factories
{
    public static class EnemyFactory
    {
        public static Dictionary<string, Enemy> Enemies { get; private set; } = new Dictionary<string, Enemy>();
        public static Enemy DefaultEnemy { get; set; }
        public static float LargestEnemyHitboxRadius { get; private set; }

        public static void Add(string name, Enemy enemy)
        {
            if (!Enemies.ContainsKey(name))
            {
                Enemies.Add(name, enemy);

                if (enemy.HitboxRadius > LargestEnemyHitboxRadius) LargestEnemyHitboxRadius = enemy.HitboxRadius;
                if (DefaultEnemy == null) DefaultEnemy = enemy;
            }
        }

        public static Enemy GetEnemy(string name)
        {
            Enemy enemyToCopy = DefaultEnemy;

            if (Enemies.ContainsKey(name)) enemyToCopy = Enemies[name];

            return enemyToCopy == null ? null : new Enemy(enemyToCopy.Sprite, enemyToCopy.HitboxRadius, enemyToCopy.Scale, enemyToCopy.Speed, enemyToCopy.Health, enemyToCopy.Armor, enemyToCopy.Damage, enemyToCopy.LayerDepth);
        }
    }
}
