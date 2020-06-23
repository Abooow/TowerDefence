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
            OldCellPosition = new Point((int)(Position.X / spacePartitioner.GridSize.X), (int)(Position.Y / spacePartitioner.GridSize.Y));
            Position = newPosition;
            CellPosition = new Point((int)(Position.X / spacePartitioner.GridSize.X), (int)(Position.Y / spacePartitioner.GridSize.Y));
        }

        public void AddToWorld()
        {
            if (!haveBeenAdded)
            {
                if (OldCellPosition != null)
                {
                    Point oldCellPos = (Point)OldCellPosition;
                    spacePartitioner.Grids[][]

                }

                haveBeenAdded = true;
            }
        }

        public void RemoveFromWorld()
        {
            if (haveBeenAdded)
            {
                haveBeenAdded = false;
            }
        }

        public sealed override int GetHashCode() => hash;
    }
}
