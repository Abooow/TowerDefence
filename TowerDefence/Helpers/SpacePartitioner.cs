using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Moldels;

namespace TowerDefence.Helpers
{
    public class SpacePartitioner
    {
        public HashSet<SpaceUnit>[][] Cells { get; }
        public HashSet<SpaceUnit> OutOfBoundsUnits { get; }
        public Point TotalCells { get; }
        public Point CellSize { get; }

        public SpacePartitioner(Point totalCells, Point cellSize)
        {
            TotalCells = totalCells;
            CellSize = cellSize;

            Cells = new HashSet<SpaceUnit>[totalCells.Y][];
            OutOfBoundsUnits = new HashSet<SpaceUnit>();

            for (int i = 0; i < totalCells.Y; i++)
                Cells[i] = new HashSet<SpaceUnit>[totalCells.X];

            for (int y = 0; y < totalCells.Y; y++)
            {
                for (int x = 0; x < totalCells.X; x++)
                {
                    Cells[y][x] = new HashSet<SpaceUnit>();
                }
            }
        }

        public bool IsInsideWorld(Vector2 point, out Point cellPos)
        {
            cellPos = new Point((int)(point.X / CellSize.X), (int)(point.Y / CellSize.Y));
            return (point.X >= 0 && point.Y >= 0 && cellPos.X < TotalCells.X && cellPos.Y < TotalCells.Y);
        }

        public void AddPoint(SpaceUnit unit)
        {
            unit.AddToWorld();
        }

        public void RemovePoint(SpaceUnit unit)
        {
            unit.RemoveFromWorld();
        }

        public ICollection<SpaceUnit> Query(Vector2 point)
        {
            if (IsInsideWorld(point, out Point cellPos)) return Cells[cellPos.Y][cellPos.X];
            else return new List<SpaceUnit>(0);
        }

        public void Query(Vector2 position, float radius, Action<IEnumerable<SpaceUnit>> actionOnFoundUnits)
        {
            Point start = new Point(
                (int)Math.Floor((position.X - radius) / CellSize.X), 
                (int)Math.Floor((position.Y - radius) / CellSize.Y));
            Point end = new Point(
                (int)Math.Ceiling((position.X + radius) / CellSize.X), 
                (int)Math.Ceiling((position.Y + radius) / CellSize.Y));

            start = Vector2.Clamp(start.ToVector2(), Vector2.Zero, TotalCells.ToVector2()).ToPoint();
            end = Vector2.Clamp(end.ToVector2(), Vector2.Zero, TotalCells.ToVector2()).ToPoint();

            for (int y = start.Y; y < end.Y; y++)
            {
                for (int x = start.X; x < end.X; x++)
                {
                    if (Circle.Intercects(position, radius, new Rectangle(new Point(x, y) * CellSize, CellSize)))
                        actionOnFoundUnits.Invoke(Cells[y][x]);
                }
            }
        }
    }
}
