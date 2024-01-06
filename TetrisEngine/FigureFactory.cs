using TetrisEngine.Figure;
using TetrisEngine.Figure.Models;

using static TetrisEngine.Figure.AbstractFigure;

namespace TetrisEngine
{
	internal class FigureFactory
	{
		private readonly Random _random = new Random();
		private const int figuriesCount = 7;

		public AbstractFigure GetFigure(Position startPosition)
		{
			var figure = (Figuries)_random.Next(figuriesCount);

			return new CubeFigure(startPosition);
			//return figure switch
			//{
			//Figuries.I => new AbstractFigure(),
			//	case Figuries.J:
			//	break;
			//case Figuries.L:
			//	break;
			//case Figuries.O:
			//	break;
			//case Figuries.S:
			//	break;
			//case Figuries.T:
			//	break;
			//case Figuries.Z:
			//	break;
			//default:
			//	throw new Exception();
			//}
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
