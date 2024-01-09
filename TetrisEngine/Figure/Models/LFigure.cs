namespace TetrisEngine.Figure.Models
{
	internal class LFigure : AbstractFigure
	{
		public LFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[1].Y += 1;
			Segments[2].Y += 2;
			Segments[3].Y += 2;
			Segments[3].X += 1;

			LeftPos = Segments[0].X;
			RightPos = Segments[3].X;
			BottomPos = Segments[3].Y;
		}

		public override void Rotate(RotateDirection direction) => throw new NotImplementedException();
		protected override Position[] GetSegmentsDisplacement(RotateDirection direction) => throw new NotImplementedException();
	}
}
