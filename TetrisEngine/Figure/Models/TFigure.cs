namespace TetrisEngine.Figure.Models
{
	internal class TFigure : AbstractFigure
	{
		public TFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[0].X -= 1;
			Segments[2].X += 1;
			Segments[3].Y += 1;
		}

		public override Position[] GetRotateDisplacement()
		{
			return (_rotateState) switch
			{
				0 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-1, -1)],
				1 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(1, -1)],
				2 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(1, 1)],
				3 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(-1, 1)],
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}
