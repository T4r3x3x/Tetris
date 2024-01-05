using System.Drawing;

namespace TetrisEngine
{
	public class Cell
	{
		public bool Filled;
		public Color Color;

		public Cell(bool filled, Color color)
		{
			Filled = filled;
			Color = color;
		}
	}
}