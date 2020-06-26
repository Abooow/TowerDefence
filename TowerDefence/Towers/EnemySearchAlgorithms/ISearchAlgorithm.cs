using System;
using System.Collections.Generic;
using TowerDefence.Helpers;
using TowerDefence.Moldels;

namespace TowerDefence.Towers.EnemySearchAlgorithms
{
    public interface ISearchAlgorithm
    {
        Tower Tower { get; set; }
        Enemy FoundEnemy { get; set; }
        void FindEnemies(IEnumerable<SpaceUnit> foundUnits);
    }
}
