namespace TetrisEngine.Figure.Models
{
	internal class OFigure : AbstractFigure
	{
		public OFigure(Position startPosition) : base(startPosition) { }

		protected override Position[] _segmentsLocalPosition => [new(0, 0), new(1, 0), new(0, 1), new(1, 1)];

		//при повороте фигуры ничего не меняется
		public override Position[] GetRotationDisplacement() => [new Position(0, 0), new Position(0, 0), new Position(0, 0), new Position(0, 0)];
	}
}
