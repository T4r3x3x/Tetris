using TetrisEngine;

namespace ConsoleFrontend.Input
{
	public class DefaultInputHandler : IInputHandler
	{
		private readonly GameProducer _game;

		public DefaultInputHandler(GameProducer game)
		{
			_game = game;
		}

		public async void InputHandle(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.A:
					await _game.MoveFigureLeft();
					break;
				case ConsoleKey.D:
					await _game.MoveFigureRight();
					break;
				case ConsoleKey.W:
					await _game.RotateFigure();
					break;
				case ConsoleKey.S:
					await _game.MoveFigureDown();
					break;
				case ConsoleKey.Escape:
					_game.Pause();
					break;
				case ConsoleKey.Enter:
					_game.Resume();
					break;
			}
		}
	}
}
