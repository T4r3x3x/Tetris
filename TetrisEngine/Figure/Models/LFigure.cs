namespace TetrisEngine.Figure.Models
{
	internal class LFigure : AbstractFigure
	{
		public LFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[1].Y += 1;
			Segments[2].Y += 2;
			Segments[3].Y += 2;
			Segments[3].X += 1;
		}

		public override Position[] GetRotateDisplacement()
		{
			return (_rotateState) switch
			{
				0 => [new Position(1, 1), new Position(0, 0), new Position(-1, -1), new Position(-2, 0)],
				1 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(0, -2)],
				2 => [new Position(-1, -1), new Position(0, 0), new Position(1, 1), new Position(2, 0)],
				3 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(0, 2)],
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}
