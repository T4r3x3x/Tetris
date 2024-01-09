namespace TetrisEngine.Figure.Models
{
	internal class LineFigure : AbstractFigure
	{

		//сохраняем поворот фигуры: 0 - 0 градусов, 1 - 90, 2 - 180, 3 - 270
		private int _rotateSate = 3;

		public LineFigure(Position startPosition)
		{
			for (int i = 0; i < Segments.Length; i++)
			{
				Segments[i] = startPosition;
				Segments[i].Y += i;
			}

			LeftPos = Segments[3].X;
			RightPos = Segments[0].X;
			BottomPos = Segments[3].Y;
		}


		public override void Rotate(RotateDirection direction)
		{
			var coef = (int)direction;
			var displacement = GetSegmentsDisplacement(direction);
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] += coef * displacement[i];

			_rotateSate += coef;
			if (_rotateSate > 3)
				_rotateSate = 0;
			else if (_rotateSate < 0)
				_rotateSate = 3;
		}

		protected override Position[] GetSegmentsDisplacement(RotateDirection direction)
		{
			if (direction == RotateDirection.right)
				return _rotateSate switch
				{
					0 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 2)],
					1 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, -2)],
					2 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 2)],
					3 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, -2)],
					_ => throw new ArgumentOutOfRangeException()
				};
			else
				return _rotateSate switch
				{
					0 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, -2)],
					1 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 2)],
					2 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, -2)],
					3 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 2)],
					_ => throw new ArgumentOutOfRangeException()
				};
		}
	}
}