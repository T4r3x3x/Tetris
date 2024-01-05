using TetrisEngine.Figure;

namespace TetrisEngine
{
	public class GameProducer
	{
		const int Width = 3, Height = 3;
		private const int figuriesCount = 7;
		private bool isGameOver = false;
		private bool isPause = false;
		private readonly int Delay;
		private Random _random;
		private AbstractFigure _figure;
		private FigureFactory _figureFactory;
		Cell[,] field = new Cell[Width, Height];

		public GameProducer(int delay)
		{
			Delay = delay;
			_figureFactory = new FigureFactory();
		}


		#region API

		public void Start()
		{
			ProduceGame();
		}


		public void Pause()
		{

		}
		public void Resume()
		{

		}

		public void MoveFigureLeft()
		{

		}
		public void MoveFigureRight()
		{

		}
		public void MoveFigureDown()
		{

		}

		public void RotateFigure()
		{

		}
		#endregion


		private AbstractFigure GetFigure()
		{
			var figureNumber = _random.Next(figuriesCount);

			return;
		}

		private void ProduceGame()
		{
			while (!isGameOver)
			{
				if (!isPause)
				{
					Thread.Sleep(Delay);
				}
			}
		}
	}
}
