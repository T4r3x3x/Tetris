namespace TetrisEngine.Models
{
    internal readonly record struct Position(int X, int Y)
    {
        public static Position operator +(Position pos1, Position pos2) => new Position(pos1.X + pos2.X, pos1.Y + pos2.Y);

        public static Position operator -(Position pos1, Position pos2) => new Position(pos1.X - pos2.X, pos1.Y - pos2.Y);

        public static Position operator *(int a, Position pos) => new Position(pos.X * a, pos.Y * a);
    }
}