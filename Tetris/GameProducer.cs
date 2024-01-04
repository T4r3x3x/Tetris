using Tetris.Input;

namespace Tetris
{
	internal class GameProducer
	{
		private bool isGameOver = false;
		private bool isPause = false;
		private readonly int Delay;
		private readonly IInputReader _inputReader;

		public GameProducer(int delay, IInputReader inputReader)
		{
			Delay = delay;
			_inputReader = inputReader;
			_inputReader.OnNewInput
		}

		public void StartGame()
		{
			ProduceGame();
		}


		public void Pause()
		{

		}

		private char GetNewInput()
		{
			return
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
