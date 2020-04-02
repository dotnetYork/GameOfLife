namespace GameOfLife
{
    public readonly struct Cell
    {
        public readonly int X { get; }
        public readonly int Y { get; }

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
