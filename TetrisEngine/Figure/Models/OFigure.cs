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

			LeftPos = Segments[0].X;
			RightPos = Segments[1].X;
			BottomPos = Segments[3].Y;
		}

		public override void Rotate(RotateDirection direction) {/*квадрат при вращении никак не изменяется */}

		protected override Position[] GetSegmentsDisplacement(RotateDirection direction) => throw new NotImplementedException();
	}
}
