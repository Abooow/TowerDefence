using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Helpers;

namespace TowerDefence.Moldels
{
    public abstract class SpaceUnit
    {
        private static int hash = int.MinValue;

        public Vector2 Position { get; set; }
        public Point CellPosition { get; private set; }
        public Point? OldCellPosition { get; private set; }

        private SpacePartitioner spacePartitioner;

        private bool wasInsideWorld;
        private bool haveBeenAdded;

        public SpaceUnit(SpacePartitioner spacePartitioner)
        {
            this.spacePartitioner = spacePartitioner;
            OldCellPosition = null;
            hash++;
        }

        public bool IsInsideWorld => spacePartitioner.IsInsideWorld(Position);

        public void Move(Vector2 newPosition)
        {
            wasInsideWorld = IsInsideWorld;
            OldCellPosition = new Point((int)(Position.X / spacePartitioner.GridSize.X), (int)(Position.Y / spacePartitioner.GridSize.Y));
            Position = newPosition;
            CellPosition = new Point((int)(Position.X / spacePartitioner.GridSize.X), (int)(Position.Y / spacePartitioner.GridSize.Y));
            bool isInsideWorld = IsInsideWorld;

            Point oldCellPos = (Point)OldCellPosition;
            if (CellPosition != oldCellPos || (wasInsideWorld != isInsideWorld))
            {
                if (wasInsideWorld != isInsideWorld)
                {

                }

                spacePartitioner.Grids[oldCellPos.Y][oldCellPos.X].Remove(this);
                if (IsInsideWorld) spacePartitioner.Grids[CellPosition.Y][CellPosition.X].Add(this);

            }
        }

        public void AddToWorld()
        {
            if (!haveBeenAdded)
            {
                if (IsInsideWorld) spacePartitioner.Grids[CellPosition.Y][CellPosition.X].Add(this);
                else spacePartitioner.OutOfBoundsUnits.Add(this);

                haveBeenAdded = true;
            }
        }

        public void RemoveFromWorld()
        {
            if (haveBeenAdded)
            {
                if (IsInsideWorld) spacePartitioner.Grids[CellPosition.Y][CellPosition.X].Remove(this);
                else spacePartitioner.OutOfBoundsUnits.Remove(this);

                haveBeenAdded = false;
            }
        }

        public sealed override int GetHashCode() => hash;
    }
}
