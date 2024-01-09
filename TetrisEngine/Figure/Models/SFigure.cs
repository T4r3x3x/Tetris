namespace TetrisEngine.Figure.Models
{
	internal class SFigure : AbstractFigure
	{
		public SFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] = startPosition;

			Segments[1].X += 1;
			Segments[2].Y += 1;
			Segments[2].X -= 1;
			Segments[3].Y += 1;

			LeftPos = Segments[3].X;
			RightPos = Segments[1].X;
			BottomPos = Segments[3].Y;
		}

		public override void Rotate(RotateDirection direction) => throw new NotImplementedException();
		protected override Position[] GetSegmentsDisplacement(RotateDirection direction) => throw new NotImplementedException();
	}
}
