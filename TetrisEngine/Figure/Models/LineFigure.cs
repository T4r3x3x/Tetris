namespace TetrisEngine.Figure.Models
{
	internal class LineFigure : AbstractFigure
	{

		public LineFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
			{
				Segments[i] = startPosition;
				Segments[i].X += i;
			}
		}

		public override Position[] GetRotateDisplacement()
		{
			return (_rotateState % 2) switch
			{
				0 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 2)],
				1 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, -2)],
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}