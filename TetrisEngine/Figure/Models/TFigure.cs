using System.Drawing;

namespace TetrisEngine.Figure.Models
{
	internal class TFigure : AbstractFigure
	{
		public TFigure(Position startPosition) : base(startPosition) { }

		public override Color Color => Color.Purple;

		protected override Position[] _segmentsLocalPosition => [new(-1, 0), new(0, 0), new(1, 0), new(0, 1)];

		public override Position[] GetRotationDisplacement()
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
