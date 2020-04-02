using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using Xunit;

namespace tests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "The next generation of cells is dead if we currently don't have any live cells.")]
        public void EmptyCellsGivesEmptyCells()
        {
            var universe = new Universe(new List<Cell>());

            universe = universe.NextGeneration();

            Assert.Empty(universe.LivingCells());
        }

        [Fact(DisplayName = "Cell dies if all of its neighbours are off")]
        public void CellDiesIfNoNeighboursAreOn()
        {
            var universe = new Universe(new List<Cell> { new Cell(1, 1) });

            universe = universe.NextGeneration();

            Assert.Empty(universe.LivingCells());
        }

        [Fact(DisplayName = "Correct state is returned when universe is created with an initial state")]
        public void UniverseReturnsCurrentState()
        {
            var expectedCell = new Cell(1, 1);
            var universe = new Universe(new List<Cell> { expectedCell });

            Assert.Single(universe.LivingCells());

            Assert.Equal(expectedCell.X, universe.LivingCells()[0].X);
            Assert.Equal(expectedCell.Y, universe.LivingCells()[0].Y);
        }

        [Fact(DisplayName = "Any live cell with two neighbours lives on to the next generation")]
        public void LiveCellWith2NeighboursLivesOn()
        {
            //  X      .
            //  X ->   X
            //  X      .
            var universe = new Universe(new List<Cell> { new Cell(1, 1), new Cell(1, 2), new Cell(1, 3) });

            universe = universe.NextGeneration();

            var actualCell = universe.LivingCells().SingleOrDefault(cell => cell.X == 1 && cell.Y == 2);


            Assert.Equal(1, actualCell.X);
            Assert.Equal(2, actualCell.Y);
        }

        [Fact(DisplayName = "Any live cell with three live neighbours lives on to the next generation")]
        public void LiveCellWith3NeighboursLivesOn()
        {
            //     X      .
            //  X  X -> X X
            //     X      .
            var universe = new Universe(new List<Cell> { new Cell(1, 1), new Cell(1, 2), new Cell(1, 3), new Cell(0, 2) });

            universe = universe.NextGeneration();

            var actual = universe.LivingCells();
            AssertCellExists(actual, 1, 1);
            AssertCellExists(actual, 1, 2);
        }

        [Fact(DisplayName = "Any live cell with more than three live neighbours dies, as if by overpopulation.")]
        public void LiveCellWithMoreThan3Neighbours()
        {
            //     0  1     0 1
            //  1  X  X     X X
            //  2  X  X  -> . .
            //  3     X       X
            var universe = new Universe(new List<Cell> { new Cell(1, 1), new Cell(1, 2), new Cell(1, 3), new Cell(0, 2), new Cell(0, 1) });

            universe = universe.NextGeneration();

            var actual = universe.LivingCells();
            AssertNotCellExists(actual, 0, 2);
            AssertNotCellExists(actual, 1, 2);
        }

        [Fact(DisplayName = "Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.")]
        public void DeadCellWithThreeLiveNeighboursBecomesAlive()
        {
            //    0  1  2     0 1 2
            //  1 .  X  .     . . .
            //  2 .  X  .  -> X X X
            //  3 .  X  .     . . .
            var universe = new Universe(new List<Cell> { new Cell(1, 1), new Cell(1, 2), new Cell(1, 3) });

            universe = universe.NextGeneration();

            var actual = universe.LivingCells();
            AssertCellExists(actual, 0, 2);
            AssertCellExists(actual, 2, 2);
        }

        private static void AssertNotCellExists(IReadOnlyList<Cell> actual, int x, int y)
        {
            Assert.DoesNotContain(new Cell(x, y), actual);
        }

        private void AssertCellExists(IReadOnlyList<Cell> cells, int x, int y)
        {
            Assert.Contains(new Cell(x, y), cells);
        }
    }
}
