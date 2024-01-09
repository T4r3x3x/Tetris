﻿namespace TetrisEngine.Figure.Models
{
	internal class TFigure : AbstractFigure
	{
		public TFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[0].X -= 1;
			Segments[2].X += 1;
			Segments[3].Y += 1;

			LeftPos = Segments[0].X;
			RightPos = Segments[2].X;
			BottomPos = Segments[3].Y;
		}
		public override void Rotate(RotateDirection direction) => throw new NotImplementedException();
		protected override Position[] GetSegmentsDisplacement(RotateDirection direction) => throw new NotImplementedException();
	}
}
