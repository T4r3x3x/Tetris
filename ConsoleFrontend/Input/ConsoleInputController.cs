using TetrisEngine;

namespace ConsoleFrontend.Input
{
	public class ConsoleInputController
	{
		private readonly GameProducer _game;

		public ConsoleInputController(GameProducer game) => _game = game;

		public void Reading()
		{
			while (true)
			{
				var key = Console.ReadKey().Key;
				InputHandler(key);
			}
		}

		public void InputHandler(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.A:
					_game.MoveFigureLeft();
					break;
				case ConsoleKey.D:
					_game.MoveFigureRight();
					break;
				case ConsoleKey.W:
					_game.RotateFigure();
					break;
				case ConsoleKey.S:
					_game.MoveFigureDown();
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