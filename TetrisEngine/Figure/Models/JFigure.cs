using System.Drawing;

namespace TetrisEngine.Figure.Models
{
	public class JFigure : AbstractFigure
	{
		public JFigure(Position startPosition) : base(startPosition) { }

		public override Color Color => Color.Blue;

		protected override Position[] _segmentsLocalPosition => [new(0, 0), new(0, 1), new(0, 2), new(-1, 2)];

		public override Position[] GetRotationDisplacement()
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
