using System.Drawing;

namespace TetrisEngine
{
	public class Cell : ICloneable
	{
		public bool IsFilled;
		public Color Color;

		public Cell(bool filled, Color color)
		{
			IsFilled = filled;
			Color = color;
		}

		public static readonly Color DefaultColor = Color.Black;

		public object Clone() => new Cell(IsFilled, Color);
	}
}