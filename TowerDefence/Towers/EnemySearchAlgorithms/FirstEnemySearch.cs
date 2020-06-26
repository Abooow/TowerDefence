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
        public Tower Tower { get; set; }
        public Enemy FoundEnemy { get; set; }

        public void FindEnemies(IEnumerable<SpaceUnit> foundUnits)
        {
            int lastWayPoint = FoundEnemy?.AiController.WayPointIndex ?? 0;
            //float shortestDistanceToWayPoint = float.PositiveInfinity;

            foreach (Enemy enemy in foundUnits)
            {
                if (Circle.Intercects(Tower.Position, Tower.RangeRadius, enemy.Position, enemy.HitboxRadius))
                {
                    if (enemy.AiController.WayPointIndex > lastWayPoint)
                    {
                        lastWayPoint = enemy.AiController.WayPointIndex;
                        //shortestDistanceToWayPoint = enemy.AiController.DistanceToNextWayPoint;
                        FoundEnemy = enemy;
                    }
                }
            }
        }
    }
}
