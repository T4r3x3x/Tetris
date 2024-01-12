using System.Drawing;

namespace TetrisEngine.Figure
{
	public abstract class AbstractFigure
	{
		private const int SegmentsCount = 4;

		//сохраняем поворот фигуры: 0 - 0 градусов, 1 - 90, 2 - 180, 3 - 270
		protected int _rotateState = 0;

		protected AbstractFigure(Position createPosition)
		{
			for (int i = 0; i < _segmentsPosition.Length; i++)
				_segmentsPosition[i] = createPosition + _segmentsLocalPosition[i];
		}

		protected abstract Position[] _segmentsLocalPosition { get; }

		protected Position[] _segmentsPosition = new Position[SegmentsCount];

		public abstract Color Color { get; }

		public IEnumerable<Position> SegmentsPosition => _segmentsPosition;

		public bool IsBelong(Position segment)
		{
			foreach (var figureSegment in _segmentsPosition)
				if (figureSegment == segment)
					return true;

			return false;
		}

		public void Move(Position moveVector)
		{
			for (int i = 0; i < _segmentsPosition.Length; i++)
				_segmentsPosition[i] += moveVector;
		}

		public void Rotate()
		{
			var displacement = GetRotationDisplacement();
			for (int i = 0; i < _segmentsPosition.Length; i++)
				_segmentsPosition[i] += displacement[i];

			if (++_rotateState > 3)
				_rotateState = 0;
		}

		public abstract Position[] GetRotationDisplacement();
	}
}