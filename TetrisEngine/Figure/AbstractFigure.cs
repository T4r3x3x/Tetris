namespace TetrisEngine.Figure
{
	public abstract class AbstractFigure
	{
		private const int SegmentsCount = 4;
		public Position[] Segments = new Position[SegmentsCount]; //todo переименовать в segmentsPosition

		public bool BelongToFigure(Position segment)
		{
			foreach (var figureSegment in Segments)
				if (figureSegment == segment)
					return true;

			return false;
		}
		public abstract void Rotate(RotateDirection direction);
		protected abstract Position[] GetSegmentsDisplacement(RotateDirection direction);
	}

	public enum RotateDirection
	{
		left = -1, right = 1,
	}
}