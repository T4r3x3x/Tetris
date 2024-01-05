using TetrisEngine;

namespace ConsoleFrontend.Display
{
	public class ConsoleDisplay
	{
		public void Display(Cell[,] gameField)
		{
			int heigth = gameField.GetLength(0);
			Console.Clear();
			DisplayVerticalLine(gameField.Length);
			for (int i = 0; i < heigth; i++)
				DisplayRow((Cell[])gameField.GetValue(i));

			DisplayVerticalLine(gameField.Length);
		}

		private void DisplayVerticalLine(int width)
		{
			for (int i = 0; i < width + 2; i++)
				Console.Write('-');
		}

		private void DisplayRow(Cell[] row)
		{
			Console.Write('|');
			for (int i = 0; i < row.Length; i++)
				DisplayCell(row[i]);

			Console.Write('|');
		}

		private void DisplayCell(Cell cell)
		{
			Console.ForegroundColor = (ConsoleColor)(cell.Color.ToArgb() & 0xFFFFFF);
			Console.Write(cell.Filled);
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
