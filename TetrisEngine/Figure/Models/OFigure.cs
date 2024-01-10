namespace TetrisEngine.Figure.Models
{
	internal class OFigure : AbstractFigure
	{
		public OFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[1].X += 1;
			Segments[2].Y += 1;
			Segments[3].Y += 1;
			Segments[3].X += 1;
		}
		//при повороте фигуры ничего не меняется
		public override Position[] GetRotateDisplacement() => [new Position(0, 0), new Position(0, 0), new Position(0, 0), new Position(0, 0)];
	}
}
