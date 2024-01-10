namespace TetrisEngine.Figure
{
	public abstract class AbstractFigure
	{
		private const int SegmentsCount = 4;

		//сохраняем поворот фигуры: 0 - 0 градусов, 1 - 90, 2 - 180, 3 - 270
		protected int _rotateState = 0;

		public Position[] Segments = new Position[SegmentsCount]; //todo переименовать в segmentsPosition

		public bool BelongToFigure(Position segment)
		{
			foreach (var figureSegment in Segments)
				if (figureSegment == segment)
					return true;

			return false;
		}

		public void Rotate()
		{
			var displacement = GetRotateDisplacement();
			for (int i = 0; i < Segments.Length; i++)
				Segments[i] += displacement[i];

			if (++_rotateState > 3)
				_rotateState = 0;
		}

		public abstract Position[] GetRotateDisplacement();
	}
}