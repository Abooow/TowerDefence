﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Moldels;

namespace TowerDefence.Helpers
{
    public class WorldDivider<T> where T : IPositionable
    {
        public List<PointData>[][] Grids { get; }
        public Point TotalGrids { get; }
        public Point GridSize { get; }

        private List<PointData> outOfBoundsPoints;

        public WorldDivider(Point totalGrids, Point gridSize)
        {
            TotalGrids = totalGrids;
            GridSize = gridSize;

            outOfBoundsPoints = new List<PointData>();
            Grids = new List<PointData>[totalGrids.Y][];

            for (int i = 0; i < totalGrids.Y; i++)
                Grids[i] = new List<PointData>[totalGrids.X];

            for (int y = 0; y < totalGrids.Y; y++)
            {
                for (int x = 0; x < totalGrids.X; x++)
                {
                    Grids[y][x] = new List<PointData>();
                }
            }
        }

        public void AddPoint(T point)
        {
            PointData pointData = new PointData(point);
            pointData.UpdateGridPositions(GridSize);

            if (pointData.IsInsideGrid(TotalGrids))
            {
                Grids[pointData.GridPosition.Y][pointData.GridPosition.X].Add(pointData);
            }
            else
            {
                outOfBoundsPoints.Add(pointData);
            }
        }

        public void RemovePoint(T point)
        {
            bool FindPointData(ICollection<PointData> collection, out PointData found)
            {
                foreach (var pointData in collection)
                {
                    if (pointData.Point.Equals(point))
                    {
                        found = pointData;
                        return true;
                    }
                }

                found = null;
                return false;
            }

            Point gridPos = new Point((int)(point.Position.X / GridSize.X), (int)(point.Position.Y / GridSize.Y));

            if (point.Position.X >= 0 && point.Position.Y >= 0 && gridPos.X < TotalGrids.X && gridPos.Y < TotalGrids.Y)
            {
                if (FindPointData(Grids[gridPos.Y][gridPos.X], out PointData found))
                {
                    Grids[gridPos.Y][gridPos.X].Remove(found);
                }
                // bug: Failed to find point, solution: search all nearby cells.
                else
                {
                    Point start = gridPos - new Point(1);
                    Point end = gridPos + new Point(2);
                    start = Vector2.Clamp(start.ToVector2(), Vector2.Zero, TotalGrids.ToVector2()).ToPoint();
                    end = Vector2.Clamp(end.ToVector2(), Vector2.Zero, TotalGrids.ToVector2()).ToPoint();

                    for (int y = start.Y; y < end.Y; y++)
                    {
                        for (int x = start.X; x < end.X; x++)
                        {
                            if (y == gridPos.Y && x == gridPos.X) continue;

                            if (FindPointData(Grids[y][x], out PointData newFound))
                            {
                                Grids[y][x].Remove(newFound);
                                return;
                            }
                        }
                    }
                }
            }
            else if (FindPointData(outOfBoundsPoints, out PointData found))
            {
                outOfBoundsPoints.Remove(found);
            }
        }

        public void Update()
        {
            // outOfBoundsPoints points.
            for (int i = outOfBoundsPoints.Count - 1; i >= 0; i--)
            {
                PointData point = outOfBoundsPoints[i];

                point.UpdateGridPositions(GridSize);

                if (point.IsInsideGrid(TotalGrids))
                {
                    Grids[point.GridPosition.Y][point.GridPosition.X].Add(point);
                    outOfBoundsPoints.Remove(point);
                }
            }

            // grids points.
            for (int y = 0; y < TotalGrids.Y; y++)
            {
                for (int x = 0; x < TotalGrids.X; x++)
                {
                    for (int i = Grids[y][x].Count - 1; i >= 0; i--)
                    {
                        PointData point = Grids[y][x][i];

                        point.UpdateGridPositions(GridSize);
                        point.LastPosition = Grids[y][x][i].Point.Position;

                        if (point.IsInsideGrid(TotalGrids))
                        {
                            if (point.GridPosition != point.LastGridPosition)
                            {
                                Grids[point.LastGridPosition.Y][point.LastGridPosition.X].Remove(point);
                                Grids[point.GridPosition.Y][point.GridPosition.X].Add(point);
                            }
                        }
                        else
                        {
                            Grids[point.LastGridPosition.Y][point.LastGridPosition.X].Remove(point);
                            outOfBoundsPoints.Add(point);
                        }
                    }
                }
            }
        }

        public List<PointData> Query(Vector2 point)
        {
            Point gridPos = new Point((int)Math.Floor(point.X / GridSize.X), (int)Math.Floor(point.Y / GridSize.Y));

            if (point.X >= 0 && point.Y >= 0 && gridPos.X < TotalGrids.X && gridPos.Y < TotalGrids.Y) return Grids[gridPos.Y][gridPos.X];
            else return new List<PointData>();
        }

        public List<List<PointData>> Query(Vector2 position, float radius)
        {
            List<List<PointData>> points = new List<List<PointData>>();

            Point start = new Point((int)Math.Floor((position.X - radius) / GridSize.X), (int)Math.Floor((position.Y - radius) / GridSize.Y));
            Point end = new Point((int)Math.Ceiling((position.X + radius) / GridSize.X), (int)Math.Ceiling((position.Y + radius) / GridSize.Y));

            start = Vector2.Clamp(start.ToVector2(), Vector2.Zero, TotalGrids.ToVector2()).ToPoint();
            end = Vector2.Clamp(end.ToVector2(), Vector2.Zero, TotalGrids.ToVector2()).ToPoint();

            for (int y = start.Y; y < end.Y; y++)
            {
                for (int x = start.X; x < end.X; x++)
                {
                    if (Circle.Intercects(position, radius, new Rectangle(new Point(x, y) * GridSize, GridSize))) points.Add(Grids[y][x]);
                }
            }

            return points;
        }

        public class PointData
        {
            public T Point { get; }
            public Vector2 LastPosition { get; set; }
            public Point GridPosition { get; private set; }
            public Point LastGridPosition { get; private set; }

            public PointData(T point)
            {
                Point = point;
                LastPosition = point.Position;
            }

            public bool IsInsideGrid(Point totalGrids)
            {
                return Point.Position.X >= 0 && Point.Position.Y >= 0 && GridPosition.X < totalGrids.X && GridPosition.Y < totalGrids.Y;
            }

            public void UpdateGridPositions(Point gridSize)
            {
                LastGridPosition = new Point((int)(LastPosition.X / gridSize.X), (int)(LastPosition.Y / gridSize.Y));
                GridPosition = new Point((int)(Point.Position.X / gridSize.X), (int)(Point.Position.Y / gridSize.Y));
            }
        }
    }
}
