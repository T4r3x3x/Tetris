using TetrisEngine.Figure;
using TetrisEngine.Input;

namespace TetrisEngine
{
	public class GameProducer
	{
		private const int figuriesCount = 7;
		private bool isGameOver = false;
		private bool isPause = false;
		private readonly int Delay;
		private readonly IInputReader _inputReader;
		private Random _random;
		private AbstractFigure _figure;

		public GameProducer(int delay, IInputReader inputReader)
		{
			Delay = delay;
			_inputReader = inputReader;
			_inputReader.OnNewInput += InputHandler;
		}

		public void StartGame()
		{
			ProduceGame();
		}


		public void Pause()
		{

		}

		private void InputHandler(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.A:
					break;
				case ConsoleKey.D:
					break;
				case ConsoleKey.W:
					break;
				case ConsoleKey.S:
					break;
				case ConsoleKey.Escape:
					break;
			}
		}

		private AbstractFigure GetFigure()
		{
			var figureNumber = _random.Next(figuriesCount);
			return;
		}

		public void ProduceGame()
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
