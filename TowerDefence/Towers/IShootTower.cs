using TowerDefence.Towers.EnemySearchAlgorithms;

namespace TowerDefence.Towers
{
    public interface IShootTower
    {
        float BarrelLength { get; }
        float ShootRate { get; set; }
        float ShootTimer { get; set; }
        ISearchAlgorithm SearchAlgorithm { get; set; }
    }
}
