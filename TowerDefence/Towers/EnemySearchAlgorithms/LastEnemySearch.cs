using System.Collections.Generic;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers.EnemySearchAlgorithms
{
    public class LastEnemySearch : ISearchAlgorithm
    {
        public Enemy FindEnemy(Tower tower, List<List<SpacePartitioner<Enemy>.PointData>> nearbyEnemies)
        {
            int firstWayPoint = int.MaxValue;
            //float shortestDistanceToWayPoint = float.PositiveInfinity;
            Enemy foundEnemy = null;

            foreach (var enemyList in nearbyEnemies)
            {
                foreach (var enemyPoint in enemyList)
                {
                    Enemy enemy = enemyPoint.Point;
                    if (Circle.Intercects(tower.Position, tower.RangeRadius, enemy.Position, enemy.HitboxRadius))
                    {
                        if (enemy.AiController.WayPointIndex < firstWayPoint)
                        {
                            firstWayPoint = enemy.AiController.WayPointIndex;
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
