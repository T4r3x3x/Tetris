using TetrisEngine.Figure;

namespace TetrisEngine
{
	internal class FigureFactory
	{
		public AbstractFigure GetFigure(Figuries figure)
		{
			return figure switch
			{
			Figuries.I => new AbstractFigure(),
				case Figuries.J:
				break;
			case Figuries.L:
				break;
			case Figuries.O:
				break;
			case Figuries.S:
				break;
			case Figuries.T:
				break;
			case Figuries.Z:
				break;
			default:
				throw new Exception();
			}
		}
	}
}
