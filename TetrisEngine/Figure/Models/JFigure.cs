namespace TetrisEngine.Figure.Models
{
	internal class JFigure : AbstractFigure
	{
		public JFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[1].Y += 1;
			Segments[2].Y += 2;
			Segments[3].Y += 2;
			Segments[3].X -= 1;
		}

		public override Position[] GetRotateDisplacement()
		{
			return (_rotateState) switch
			{
				0 => [new Position(1, 1), new Position(0, 0), new Position(-1, -1), new Position(0, -2)],
				1 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, 0)],
				2 => [new Position(-1, -1), new Position(0, 0), new Position(1, 1), new Position(0, 2)],
				3 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 0)],
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}
