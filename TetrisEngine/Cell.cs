using System.Drawing;

namespace TetrisEngine
{
	public class Cell
	{
		public bool IsFilled;
		public Color Color;

		public Cell(bool filled, Color color)
		{
			IsFilled = filled;
			Color = color;
		}

		public static readonly Color DefaultColor = Color.Black;
	}
}