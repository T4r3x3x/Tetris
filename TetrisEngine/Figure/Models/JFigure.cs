namespace TetrisEngine.Figure.Models
{
	internal class JFigure : AbstractFigure
	{
		public JFigure(Position startPosition)
		{
			for (int i = 0; i < segments.Length; i++)
				segments[i] = startPosition;

			segments[1].Y += 1;
			segments[2].Y += 2;
			segments[3].Y += 2;
			segments[3].X -= 1;

			LeftPos = segments[3].X;
			RightPos = segments[0].X;
			TopPos = segments[0].Y;
			BottomPos = segments[3].Y;
		}
		public override void Rotate() => throw new NotImplementedException();
	}
}
