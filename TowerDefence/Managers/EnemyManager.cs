using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Controllers;
using TowerDefence.Helpers;
using TowerDefence.Moldels;
using TowerDefence.Views;

namespace TowerDefence.Managers
{
    public class EnemyManager : IController, IView
    {
        public delegate void EnemyEvent(Enemy enemy);

        public bool Enabled { get; set; }
        public bool DebugWorldDivider { get; set; }
        public List<Enemy> Enemies { get; }

        public EnemyEvent OnEnemyReachedLastPoint;

        private WorldDivider<Enemy> worldDivider;

        private SpriteFont font;
        private float layerDepth;

        public EnemyManager()
        {
            Enemies = new List<Enemy>();

            float gridWidth = 150;
            Point mapSize = MapManager.LoadedMap.GroundTexture.Bounds.Size;
            Point totalGrids = new Point((int)Math.Ceiling(mapSize.X / gridWidth), (int)Math.Ceiling(mapSize.Y / gridWidth));
            Point gridSize = new Point((int)Math.Ceiling(mapSize.X / (float)totalGrids.X), (int)Math.Ceiling(mapSize.Y / (float)totalGrids.Y));

            worldDivider = new WorldDivider<Enemy>(totalGrids, gridSize);

            font = AssetManager.GetFont("BaseFont");
            layerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.UI);

            Enabled = true;
        }

        public void Add(Enemy enemy)
        {
            if (enemy != null && !Enemies.Contains(enemy))
            {
                Enemies.Add(enemy);
                worldDivider.AddPoint(enemy);
            }
        }

        public void Remove(Enemy enemy)
        {
            if (enemy != null && Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
                worldDivider.RemovePoint(enemy);
            }
        }

        public void Update(float deltaTime)
        {
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = Enemies[i];
                if (enemy.Update(deltaTime))
                {
                    Enemies.Remove(enemy);
                    worldDivider.RemovePoint(enemy);
                    continue;
                }
                if (enemy.HaveReachedLastWayPoint) OnEnemyReachedLastPoint?.Invoke(enemy);
            }
            worldDivider.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

            if (DebugWorldDivider)
            {
                for (int y = 0; y < worldDivider.TotalGrids.Y; y++)
                {
                    for (int x = 0; x < worldDivider.TotalGrids.X; x++)
                    {
                        int totalEnemies = worldDivider.Grids[y][x].Count;

                        spriteBatch.Draw(
                            AssetManager.GetTexture("Pixel"),
                            new Rectangle(new Point(x, y) * worldDivider.GridSize, worldDivider.GridSize),
                            null,
                            Color.Red * (totalEnemies / 10f),
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            layerDepth
                            );

                        spriteBatch.DrawString(
                            font,
                            totalEnemies.ToString(),
                            (new Point(x, y) * worldDivider.GridSize).ToVector2() + worldDivider.GridSize.ToVector2() * 0.5f,
                            Color.Beige,
                            0f,
                            font.MeasureString(totalEnemies.ToString()) * new Vector2(0.5f),
                            2f,
                            SpriteEffects.None,
                            layerDepth + 0.001f);
                    }
                }
            }
        }

        public List<WorldDivider<Enemy>.PointData> Query(Vector2 point)
        {
            return worldDivider.Query(point);
        }

        public List<List<WorldDivider<Enemy>.PointData>> Query(Vector2 position, float radius)
        {
            return worldDivider.Query(position, radius);
        }
    }
}
