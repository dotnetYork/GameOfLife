using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Universe
    {
        private IEnumerable<Cell> list;

        public Universe(IEnumerable<Cell> list)
        {
            this.list = list;
        }

        public Universe NextGeneration()
        {
            var newCells = this.list.SelectMany(cell => new [] {
                new Cell(cell.X -1, cell.Y -1),
                new Cell(cell.X, cell.Y -1),
                new Cell(cell.X +1, cell.Y -1),

                new Cell(cell.X -1, cell.Y),
                new Cell(cell.X, cell.Y),
                new Cell(cell.X +1, cell.Y),
                
                new Cell(cell.X -1, cell.Y +1),
                new Cell(cell.X, cell.Y +1),
                new Cell(cell.X +1, cell.Y +1),
            }).Distinct().Where(cell =>
            {
                var neighbours = NumberOfNeighbours(cell);
                var currentlyAlive = this.list.Contains(cell);
                return (currentlyAlive && neighbours == 2) || neighbours == 3;
            }).ToList();

            return new Universe(newCells);
        }

        private int NumberOfNeighbours(Cell currentCell)
        {
            var count = 0;
            count += this.list.Count(cell => currentCell.X - 1 == cell.X && currentCell.Y - 1 == cell.Y);
            count += this.list.Count(cell => currentCell.X == cell.X && currentCell.Y - 1 == cell.Y);
            count += this.list.Count(cell => currentCell.X + 1 == cell.X && currentCell.Y - 1 == cell.Y);

            count += this.list.Count(cell => currentCell.X - 1 == cell.X && currentCell.Y == cell.Y);
            count += this.list.Count(cell => currentCell.X + 1 == cell.X && currentCell.Y == cell.Y);

            count += this.list.Count(cell => currentCell.X - 1 == cell.X && currentCell.Y + 1 == cell.Y);
            count += this.list.Count(cell => currentCell.X == cell.X && currentCell.Y + 1 == cell.Y);
            count += this.list.Count(cell => currentCell.X + 1 == cell.X && currentCell.Y + 1 == cell.Y);

            return count;
        }

        public IReadOnlyList<Cell> LivingCells()
        {
            return this.list.ToList().AsReadOnly();
        }
    }
}
