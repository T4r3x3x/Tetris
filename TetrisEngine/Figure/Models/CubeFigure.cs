namespace TetrisEngine.Figure.Models
{
	internal class CubeFigure : AbstractFigure
	{

		public CubeFigure(Position startPosition)
		{
			for (int i = 0; i < segments.Length; i++)
				segments[i] = startPosition;

			segments[1].X += 1;
			segments[2].Y += 1;
			segments[2].Y += 1;
			segments[3].X += 1;
		}

		public override void Rotate() {/*квадрат при вращении никак не изменяется */}
	}
}
