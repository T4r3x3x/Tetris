using System.Drawing;

namespace TetrisEngine
{
	public class Cell
	{
		public char? Symbol;
		public Color Color;

		public Cell(char symbol, Color color)
		{
			Symbol = symbol;
			Color = color;
		}
	}
}