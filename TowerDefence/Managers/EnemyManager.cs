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

        public HashSet<int> ControllerGroupId { get; }
        public HashSet<int> ViewGroupId { get; }
        public bool Enabled { get; set; }
        public bool DebugWorldDivider { get; set; }

        public EnemyEvent OnEnemyReachedLastPoint;

        private SpacePartitioner spacePartitioner;

        private SpriteFont font;
        private float layerDepth;

        public EnemyManager()
        {
            float gridWidth = 140;
            Point mapSize = MapManager.LoadedMap.GroundTexture.Bounds.Size;
            Point totalGrids = new Point((int)Math.Ceiling(mapSize.X / gridWidth), (int)Math.Ceiling(mapSize.Y / gridWidth));
            Point gridSize = new Point((int)Math.Ceiling(mapSize.X / (float)totalGrids.X), (int)Math.Ceiling(mapSize.Y / (float)totalGrids.Y));

            spacePartitioner = new SpacePartitioner(totalGrids, gridSize);

            font = AssetManager.GetFont("BaseFont");
            layerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.Ui);

            DebugWorldDivider = false;
            ControllerGroupId = new HashSet<int>();
            ViewGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void Spawn(Enemy enemy)
        {
            if (enemy != null)
            {
                enemy.World = spacePartitioner;
                enemy.AiController.InitializePosition();
                enemy.AddToWorld();
            }
        }

        public void Remove(Enemy enemy)
        {
            if (enemy != null)
            {
                enemy.RemoveFromWorld();
            }
        }

        public void Update(float deltaTime)
        {
            void UpdateUnit(SpaceUnit unit)
            {
                Enemy enemy = unit as Enemy;
                if (enemy.Update(deltaTime)) enemy.RemoveFromWorld();
                if (enemy.HaveReachedLastWayPoint)
                    OnEnemyReachedLastPoint?.Invoke(enemy);
            }

            // Update units in cells.
            for (int y = 0; y < spacePartitioner.TotalCells.Y; y++)
            {
                for (int x = 0; x < spacePartitioner.TotalCells.X; x++)
                {
                    for (int i = spacePartitioner.Cells[y][x].Count - 1; i >= 0; i--)
                    {
                        UpdateUnit(spacePartitioner.Cells[y][x].ElementAt(i));
                    }
                }
            }
            // Update OutOfBoundsUnits.
            for (int i = spacePartitioner.OutOfBoundsUnits.Count - 1; i >= 0; i--)
            {
                UpdateUnit(spacePartitioner.OutOfBoundsUnits.ElementAt(i));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw units in cells.
            for (int y = 0; y < spacePartitioner.TotalCells.Y; y++)
            {
                for (int x = 0; x < spacePartitioner.TotalCells.X; x++)
                {
                    foreach (Enemy enemy in spacePartitioner.Cells[y][x]) enemy.Draw(spriteBatch);
                }
            }
            // Draw OutOfBoundsUnits.
            foreach (Enemy enemy in spacePartitioner.OutOfBoundsUnits)
            {
                enemy.Draw(spriteBatch);
            }

            if (DebugWorldDivider)
            {
                for (int y = 0; y < spacePartitioner.TotalCells.Y; y++)
                {
                    for (int x = 0; x < spacePartitioner.TotalCells.X; x++)
                    {
                        int totalEnemies = spacePartitioner.Cells[y][x].Count;

                        spriteBatch.Draw(
                            AssetManager.GetTexture("Pixel"),
                            new Rectangle(new Point(x, y) * spacePartitioner.CellSize, spacePartitioner.CellSize),
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
                            (new Point(x, y) * spacePartitioner.CellSize).ToVector2() + spacePartitioner.CellSize.ToVector2() * 0.5f,
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


        public ICollection<SpaceUnit> Query(Vector2 point)
        {
            return spacePartitioner.Query(point);
        }

        public void Query(Vector2 position, float radius, Action<IEnumerable<SpaceUnit>> actionOnFoundUnits)
        {
            spacePartitioner.Query(position, radius, actionOnFoundUnits);
        }
    }
}
