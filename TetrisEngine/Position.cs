namespace TetrisEngine
{
	public readonly struct Position
	{
		public readonly int X, Y;

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static bool operator ==(Position pos1, Position pos2) => pos1.X == pos2.X && pos1.Y == pos2.Y;

		public static Position operator +(Position pos1, Position pos2) => new Position(pos1.X + pos2.X, pos1.Y + pos2.Y);

		public static Position operator -(Position pos1, Position pos2) => new Position(pos1.X - pos2.X, pos1.Y - pos2.Y);

		public static Position operator *(int a, Position pos) => new Position(pos.X * a, pos.Y * a);

		public static bool operator !=(Position pos1, Position pos2) => (pos1.X != pos2.X || pos1.Y != pos2.Y);
	}
}