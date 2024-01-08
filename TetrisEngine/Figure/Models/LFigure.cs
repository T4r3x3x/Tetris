﻿namespace TetrisEngine.Figure.Models
{
	internal class LFigure : AbstractFigure
	{
		public LFigure(Position startPosition)
		{
			for (int i = 0; i < segments.Length; i++)
				segments[i] = startPosition;

			segments[1].Y += 1;
			segments[2].Y += 2;
			segments[3].Y += 2;
			segments[3].X += 1;

			LeftPos = segments[0].X;
			RightPos = segments[3].X;
			TopPos = segments[0].Y;
			BottomPos = segments[3].Y;
		}

		public override void Rotate() => throw new NotImplementedException();
	}
}
