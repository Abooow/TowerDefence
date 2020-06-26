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
        private static int currentHash = 0;

        public Vector2 Position { get; set; }
        public Point CellPosition { get; private set; }
        public Point? OldCellPosition { get; private set; }

        public SpacePartitioner World { get; set; }

        private int hash;
        private bool wasInsideWorld;
        private bool haveBeenAdded;

        public SpaceUnit(SpacePartitioner spacePartitioner)
        {
            this.World = spacePartitioner;
            OldCellPosition = null;
            hash = currentHash++;
        }

        public bool IsInsideWorld => World.IsInsideWorld(Position, out _);

        public void Move(Vector2 newPosition)
        {
            int enemy = hash;
            if (!haveBeenAdded)
            {
                Position = newPosition;
                CellPosition = new Point((int)(Position.X / World.CellSize.X), (int)(Position.Y / World.CellSize.Y));
                return;
            }

            if (wasInsideWorld = IsInsideWorld)
                OldCellPosition = new Point(
                    (int)(Position.X / World.CellSize.X), 
                    (int)(Position.Y / World.CellSize.Y));

            Position = newPosition;
            CellPosition = new Point((int)(Position.X / World.CellSize.X), (int)(Position.Y / World.CellSize.Y));
            bool isInsideWorld = IsInsideWorld;

            // Moved from one Cell to another.
            if (OldCellPosition.HasValue && CellPosition != OldCellPosition.Value)
            {
                World.Cells[OldCellPosition.Value.Y][OldCellPosition.Value.X].Remove(this);
                if (IsInsideWorld) World.Cells[CellPosition.Y][CellPosition.X].Add(this);
            }
            // Moved from outside world bounds to inside world bounds or vice versa.
            if (isInsideWorld != wasInsideWorld)
            {
                // Outside to inside.
                if (isInsideWorld && !wasInsideWorld)
                {
                    World.OutOfBoundsUnits.Remove(this);
                    World.Cells[CellPosition.Y][CellPosition.X].Add(this);
                }
                // Inside to outside.
                else
                {
                    World.OutOfBoundsUnits.Add(this);
                    World.Cells[OldCellPosition.Value.Y][OldCellPosition.Value.X].Remove(this);
                    OldCellPosition = null;
                }
            }
        }

        public void AddToWorld()
        {
            if (!haveBeenAdded)
            {
                Move(Position);
                if (IsInsideWorld) World.Cells[CellPosition.Y][CellPosition.X].Add(this);
                else World.OutOfBoundsUnits.Add(this);

                haveBeenAdded = true;
            }
        }

        public void RemoveFromWorld()
        {
            if (haveBeenAdded)
            {
                if (IsInsideWorld) World.Cells[CellPosition.Y][CellPosition.X].Remove(this);
                else World.OutOfBoundsUnits.Remove(this);

                haveBeenAdded = false;
            }
        }

        public sealed override int GetHashCode() => hash;
    }
}
