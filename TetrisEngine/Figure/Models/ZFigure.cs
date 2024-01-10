namespace TetrisEngine.Figure.Models
{
	internal class ZFigure : AbstractFigure
	{
		public ZFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			//  10
			//	 23	

			Segments[1].X -= 1;
			Segments[2].Y += 1;
			Segments[3].X += 1;
			Segments[3].Y += 1;
		}

		public override Position[] GetRotateDisplacement()
		{
			return (_rotateState) switch
			{
				0 => [new Position(0, 0), new Position(1, -1), new Position(-1, -1), new Position(-2, 0)],
				1 => [new Position(0, 0), new Position(1, 1), new Position(1, -1), new Position(0, -2)],
				2 => [new Position(0, 0), new Position(-1, 1), new Position(1, 1), new Position(2, 0)],
				3 => [new Position(0, 0), new Position(-1, -1), new Position(-1, 1), new Position(0, 2)],
				_ => throw new ArgumentOutOfRangeException(),
			};
		}
	}
}