using TetrisEngine.Figure;
using TetrisEngine.Figure.Models;

namespace TetrisEngine
{
	internal class FigureFactory
	{
		private readonly Random _random = new Random();
		private const int figuriesCount = 7;
		private int i = 0;
		public AbstractFigure GetFigure(Position startPosition)
		{
			var figure = (Figuries)_random.Next(figuriesCount);
			return new ZFigure(startPosition);
			return figure switch
			{
				Figuries.I => new LineFigure(startPosition),
				Figuries.J => new JFigure(startPosition),
				Figuries.L => new LFigure(startPosition),
				Figuries.O => new OFigure(startPosition),
				Figuries.S => new SFigure(startPosition),
				Figuries.T => new TFigure(startPosition),
				Figuries.Z => new ZFigure(startPosition),
				_ => throw new Exception(),
			};
		}

		public enum Figuries
		{
			I,
			J,
			L,
			O,
			S,
			T,
			Z,
		}
	}
}
