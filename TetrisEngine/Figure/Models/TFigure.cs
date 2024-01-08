namespace TetrisEngine.Figure.Models
{
	internal class TFigure : AbstractFigure
	{
		public TFigure(Position startPosition)
		{
			for (int i = 0; i < segments.Length; i++)
				segments[i] = startPosition;


			segments[0].X -= 1;
			segments[2].X += 1;
			segments[3].Y += 1;

			LeftPos = segments[0].X;
			RightPos = segments[2].X;
			TopPos = segments[0].Y;
			BottomPos = segments[3].Y;
		}
		public override void Rotate() => throw new NotImplementedException();
	}
}
