using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;
using TowerDefence.Moldels;

namespace TowerDefence.Controllers
{
    public class EnemyAiController : IController
    {
        public bool Enabled { get; set; }
        public bool HaveReachedLastWayPoint { get; private set; }

        private Enemy enemy;
        private int wayPointIndex;
        private Vector2 nextWayPoint;

        public EnemyAiController(Enemy enemy)
        {
            this.enemy = enemy;
            this.enemy.Position = GetWayPoint(0);
            nextWayPoint = GetWayPoint(++wayPointIndex);

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            // Move.
            Vector2 direction = nextWayPoint - enemy.Position;
            direction.Normalize();
            enemy.Position += direction * enemy.Speed;
            if (IsCloseTo(nextWayPoint, enemy.Speed)) nextWayPoint = GetWayPoint(++wayPointIndex);

            // Rotate.
            Vector2 delta = nextWayPoint - enemy.Position;
            enemy.Rotation = (float)Math.Atan2(delta.Y, delta.X);

            HaveReachedLastWayPoint = HaveReachedLast();
        }

        public bool HaveReachedLast() =>  MapManager.LoadedMap.EnemyPath.Length > 0 && IsCloseTo(MapManager.LoadedMap.EnemyPath.Last(), enemy.Speed);

        private bool IsCloseTo(Vector2 point, float minRange) => (enemy.Position - point).LengthSquared() <= minRange * minRange;

        private Vector2 GetWayPoint(int index)
        {
            if (index >= MapManager.LoadedMap.EnemyPath.Length) return MapManager.LoadedMap.EnemyPath[MapManager.LoadedMap.EnemyPath.Length - 1];
            else if (MapManager.LoadedMap.EnemyPath.Length > 0) return MapManager.LoadedMap.EnemyPath[index];
            else return Vector2.Zero;
        }
    }
}
