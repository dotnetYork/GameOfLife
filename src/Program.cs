using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfLife;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.CursorVisible = false;
            var universe = new Universe(
                new []
                {
                    new Cell(3,3),
                    new Cell(3,4),
                    new Cell(3,5)
                });

            for (;;)
            {
                universe = universe.NextGeneration();
                Console.Clear();
                foreach (var cell in universe.LivingCells())
                {
                    Console.SetCursorPosition(cell.X, cell.Y);
                    Console.Write("X");
                }

                await Task.Delay(500);
            }
        }
    }
}
