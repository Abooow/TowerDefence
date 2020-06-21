using System.Collections.Generic;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers.EnemySearchAlgorithms
{
    public interface ISearchAlgorithm
    {
        Enemy FindEnemy(Tower tower, List<List<WorldDivider<Enemy>.PointData>> nearbyEnemies);
    }
}
