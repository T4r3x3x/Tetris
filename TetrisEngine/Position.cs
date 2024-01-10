namespace TetrisEngine
{
	public struct Position
	{
		public int X, Y;

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static bool operator ==(Position pos1, Position pos2)
		{
			if (pos1.X == pos2.X && pos1.Y == pos2.Y)
				return true;
			return false;
		}

		public static Position operator +(Position pos1, Position pos2)
		{
			return new Position(pos1.X + pos2.X, pos1.Y + pos2.Y);
		}

		public static Position operator -(Position pos1, Position pos2)
		{
			return new Position(pos1.X - pos2.X, pos1.Y - pos2.Y);
		}
		public static Position operator *(int a, Position pos)
		{
			return new Position(pos.X * a, pos.Y * a);
		}
		public static bool operator !=(Position pos1, Position pos2)
		{
			if (pos1.X != pos2.X || pos1.Y != pos2.Y)
				return true;

			return false;
		}
	}
}