using TowerDefence.Managers;
using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public interface IShootTower
    {
        float ShootRate { get; set; }
        float ShootTimer { get; set; }
        EnemyManager EnemyManager { get; set; }
        BulletManager BulletManager { get; set; }
        ISearchAlgorithm SearchAlgorithm { get; set; }
        void FindEnemy();
    }
}
