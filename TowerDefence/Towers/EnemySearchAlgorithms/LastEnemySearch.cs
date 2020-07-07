using System.Collections.Generic;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers.EnemySearchAlgorithms
{
    public class LastEnemySearch : ISearchAlgorithm
    {
        public Tower Tower { get; set; }
        public Enemy FoundEnemy { get; set; }

        public void FindEnemies(IEnumerable<SpaceUnit> foundUnits)
        {
            int firstWayPoint = FoundEnemy?.AiController.WayPointIndex ?? int.MaxValue;
            //float shortestDistanceToWayPoint = float.PositiveInfinity;

            foreach (Enemy enemy in foundUnits)
            {
                if (enemy.AiController.WayPointIndex < firstWayPoint && 
                    Circle.Intercects(Tower.GetPosition(), Tower.RangeRadius, enemy.Position, enemy.HitboxRadius))
                {
                    
                        firstWayPoint = enemy.AiController.WayPointIndex;
                        //shortestDistanceToWayPoint = enemy.AiController.DistanceToNextWayPoint;
                        FoundEnemy = enemy;
                    
                }
            }
        }
    }
}
