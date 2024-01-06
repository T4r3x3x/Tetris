namespace TetrisEngine.Figure
{
	public abstract class AbstractFigure
	{
		private const int SegmentsCount = 4;
		public int LeftPos, TopPos, BottomPos, RightPos;
		public Position[] segments = new Position[SegmentsCount];

		public abstract void Rotate();

		public class Position
		{
			public int X, Y;

			public Position(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}
		}
	}
}
