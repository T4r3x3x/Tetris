namespace TetrisEngine.Figure.Models
{
	internal class ZFigure : AbstractFigure
	{
		public ZFigure(Position startPosition) : base(startPosition) { }

		protected override Position[] _segmentsLocalPosition => [new(0, 0), new(-1, 0), new(0, 1), new(1, 1)];

		public override Position[] GetRotationDisplacement()
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