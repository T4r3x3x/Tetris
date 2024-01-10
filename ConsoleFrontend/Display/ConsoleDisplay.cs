using System.Drawing;

using TetrisEngine;

namespace ConsoleFrontend.Display
{
	public class ConsoleDisplay
	{
		private IReadOnlyCollection<IReadOnlyCollection<Cell>> _gameFieldLastDraw;
		private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
		private CancellationToken token;
		private Task printTask;
		private bool isPrinting = false;
		private object _sync = new object();

		public ConsoleDisplay()
		{
			token = cancelTokenSource.Token;
		}

		public void Display(IReadOnlyCollection<IReadOnlyCollection<Cell>> gameField)
		{
			_gameFieldLastDraw = gameField;
			printTask = new Task(() => Print(gameField), token);
			printTask.Start();
		}

		public void StopDisplay()
		{
			cancelTokenSource.Cancel();
		}

		public void Update()
		{
			Display(_gameFieldLastDraw);
		}

		private void Print(IReadOnlyCollection<IReadOnlyCollection<Cell>> gameField)
		{
			lock (_sync)
			{
				isPrinting = true;
				Console.Clear();
				var width = gameField.First().Count;
				DisplayVerticalLine(width);
				foreach (var row in gameField)
					DisplayRow((Cell[])row);

				DisplayVerticalLine(width);
				isPrinting = false;
			}
		}

		private void DisplayVerticalLine(int width)
		{
			for (int i = 0; i < width + 2; i++)
				Console.Write('-');
			Console.WriteLine();
		}

		private void DisplayRow(Cell[] row)
		{
			Console.Write('|');
			for (int i = 0; i < row.Length; i++)
				DisplayCell(row[i]);

			Console.Write("|\n");
		}

		private void DisplayCell(Cell cell)
		{
			Console.ForegroundColor = ClosestConsoleColor(cell.Color);//(ConsoleColor)(cell.Color.ToArgb() & 0xFFFFFF);
			var symbol = cell.Filled ? '#' : ' ';
			Console.Write(symbol);
			Console.ForegroundColor = ConsoleColor.Black;
		}

		private ConsoleColor ClosestConsoleColor(Color color)
		{
			ConsoleColor ret = 0;

			double rr = color.R, gg = color.G, bb = color.B, delta = double.MaxValue;

			foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
			{
				var n = Enum.GetName(typeof(ConsoleColor), cc);
				var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
				var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
				if (t == 0.0)
					return cc;
				if (t < delta)
				{
					delta = t;
					ret = cc;
				}
			}
			return ret;
		}
	}
}