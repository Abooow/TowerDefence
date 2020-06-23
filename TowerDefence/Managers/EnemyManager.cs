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

        private SpacePartitioner<Enemy> spacePartitioner;

        private SpriteFont font;
        private float layerDepth;

        public EnemyManager()
        {
            Enemies = new List<Enemy>();

            float gridWidth = 165;
            Point mapSize = MapManager.LoadedMap.GroundTexture.Bounds.Size;
            Point totalGrids = new Point((int)Math.Ceiling(mapSize.X / gridWidth), (int)Math.Ceiling(mapSize.Y / gridWidth));
            Point gridSize = new Point((int)Math.Ceiling(mapSize.X / (float)totalGrids.X), (int)Math.Ceiling(mapSize.Y / (float)totalGrids.Y));

            spacePartitioner = new SpacePartitioner<Enemy>(totalGrids, gridSize);

            font = AssetManager.GetFont("BaseFont");
            layerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Ui);

            DebugWorldDivider = true;
            Enabled = true;
        }

        public void Spawn(Enemy enemy)
        {
            if (enemy != null && !Enemies.Contains(enemy))
            {
                enemy.AiController.InitializePosition();
                Enemies.Add(enemy);
                spacePartitioner.AddPoint(enemy);
            }
        }

        public void Remove(Enemy enemy)
        {
            if (enemy != null && Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
                spacePartitioner.RemovePoint(enemy);
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
                    spacePartitioner.RemovePoint(enemy);
                    continue;
                }
                if (enemy.HaveReachedLastWayPoint) OnEnemyReachedLastPoint?.Invoke(enemy);
            }
            spacePartitioner.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

            if (DebugWorldDivider)
            {
                for (int y = 0; y < spacePartitioner.TotalGrids.Y; y++)
                {
                    for (int x = 0; x < spacePartitioner.TotalGrids.X; x++)
                    {
                        int totalEnemies = spacePartitioner.Grids[y][x].Count;

                        spriteBatch.Draw(
                            AssetManager.GetTexture("Pixel"),
                            new Rectangle(new Point(x, y) * spacePartitioner.GridSize, spacePartitioner.GridSize),
                            null,
                            Color.Red * (totalEnemies / 100f),
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            layerDepth
                            );

                        spriteBatch.DrawString(
                            font,
                            totalEnemies.ToString(),
                            (new Point(x, y) * spacePartitioner.GridSize).ToVector2() + spacePartitioner.GridSize.ToVector2() * 0.5f,
                            Color.Black * 0.8f,
                            0f,
                            font.MeasureString(totalEnemies.ToString()) * new Vector2(0.5f),
                            2.5f,
                            SpriteEffects.None,
                            layerDepth + 0.001f);
                    }
                }
            }
        }

        public List<SpacePartitioner<Enemy>.PointData> Query(Vector2 point)
        {
            return spacePartitioner.Query(point);
        }

        public List<List<SpacePartitioner<Enemy>.PointData>> Query(Vector2 position, float radius)
        {
            return spacePartitioner.Query(position, radius);
        }
    }
}
