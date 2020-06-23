using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers.EnemySearchAlgorithms
{
    public class FirstEnemySearch : ISearchAlgorithm
    {
        public Enemy FindEnemy(Tower tower, List<List<SpacePartitioner<Enemy>.PointData>> nearbyEnemies)
        {
            int lastWayPoint = 0;
            //float shortestDistanceToWayPoint = float.PositiveInfinity;
            Enemy foundEnemy = null;

            foreach (var enemyList in nearbyEnemies)
            {
                foreach (var enemyPoint in enemyList)
                {
                    Enemy enemy = enemyPoint.Point;
                    if (Circle.Intercects(tower.Position, tower.RangeRadius, enemy.Position, enemy.HitboxRadius))
                    {
                        if (enemy.AiController.WayPointIndex > lastWayPoint)
                        {
                            lastWayPoint = enemy.AiController.WayPointIndex;
                            //shortestDistanceToWayPoint = enemy.AiController.DistanceToNextWayPoint;
                            foundEnemy = enemy;
                        }
                    }
                }
            }

            return foundEnemy;
        }
    }
}
