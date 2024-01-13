using System.Drawing;

using TetrisEngine;

namespace ConsoleFrontend.Display
{
	public class ConsoleDisplay
	{
		private const int Width = 10, Height = 20, VerticalLineWidth = 1;

		private Cell[][] _gameFieldLastFrame;
		private object _sync = new object();


		private void PrintBoundaries()
		{
			Console.Clear();
			PrintVerticalLine(Width);
			for (int i = 0; i < Height; i++)
			{
				Console.Write("|");
				for (int j = 0; j < Width; j++)
					Console.Write(" ");

				Console.Write("|\n");
			}
			PrintVerticalLine(Width);
		}

		public void Update(Cell[][] gameField)
		{
			var differenceMap = GetFramesDifference(gameField);
			if (_gameFieldLastFrame == null)
				PrintBoundaries();
			_gameFieldLastFrame = gameField;
			RedisplayGameField(gameField, differenceMap);
		}

		private List<List<bool>> GetFramesDifference(Cell[][] gameFieldCurrentFrame)
		{
			List<List<bool>> differenceMap = new List<List<bool>>();

			if (_gameFieldLastFrame == null)
			{
				differenceMap = gameFieldCurrentFrame.Select(row => row.Select(cell => cell.IsFilled | true).ToList()).ToList();
				return differenceMap;
			}

			differenceMap = gameFieldCurrentFrame.Select(row => row.Select(cell => cell.IsFilled).ToList()).ToList();
			var lastFrameFill = _gameFieldLastFrame.Select(row => row.Select(cell => cell.IsFilled).ToList()).ToList();

			for (int i = 0; i < differenceMap.Count; i++)
				for (int j = 0; j < differenceMap[0].Count; j++)
					differenceMap[i][j] = differenceMap[i][j] != lastFrameFill[i][j];

			return differenceMap;
		}


		private void RedisplayGameField(Cell[][] gameField, List<List<bool>> differenceMap)
		{
			lock (_sync)
			{
				for (int i = 0; i < gameField.Length; i++)
					for (int j = 0; j < gameField[0].Length; j++)
						if (NeedToRedisplay(differenceMap[i][j]))
							RedisplayCell(gameField[i][j], i, j);

				Console.SetCursorPosition(0, Height + 1);
			}
		}

		private bool NeedToRedisplay(bool v) => v;

		private void PrintVerticalLine(int width)
		{
			for (int i = 0; i < width + VerticalLineWidth * 2; i++)
				Console.Write('-');
			Console.WriteLine();
		}

		private void RedisplayCell(Cell cell, int top, int left)
		{
			Console.SetCursorPosition(left + 1, top + 1);

			Console.ForegroundColor = ClosestConsoleColor(cell.Color);
			var symbol = cell.IsFilled ? '#' : ' ';
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