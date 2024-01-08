namespace TetrisEngine.Figure.Models
{
	internal class LineFigure : AbstractFigure
	{
		public LineFigure(Position startPosition)
		{
			for (int i = 0; i < segments.Length; i++)
			{
				segments[i] = startPosition;
				segments[i].Y += i;
			}

			LeftPos = segments[3].X;
			RightPos = segments[0].X;
			TopPos = segments[0].Y;
			BottomPos = segments[3].Y;
		}

		public override void Rotate() => throw new NotImplementedException();
	}
}