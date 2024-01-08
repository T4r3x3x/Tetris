namespace TetrisEngine.Figure
{
	public abstract class AbstractFigure
	{
		private const int SegmentsCount = 4;
		public int LeftPos, TopPos, BottomPos, RightPos;
		public Position[] segments = new Position[SegmentsCount]; //todo переименовать в segmentsPosition

		public abstract void Rotate();

		public bool BelongToFigure(Position segment)
		{
			foreach (var figureSegment in segments)
				if (figureSegment == segment)
					return true;

			return false;
		}
	}

	public struct Position
	{
		public int X, Y;

		public Position(int x, int y)
		{
			this.X = x;
			this.Y = y;
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

		public static bool operator !=(Position pos1, Position pos2)
		{
			if (pos1.X != pos2.X || pos1.Y != pos2.Y)
				return true;

			return false;
		}
	}
}