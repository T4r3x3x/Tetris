using ConsoleFrontend.Display;

namespace ConsoleFrontend.Input
{
	public class ConsoleInputController
	{
		private readonly IInputHandler _inputHandler;
		private readonly ConsoleDisplay _consoleDisplay;
		private bool isListening = true;

		public ConsoleInputController(IInputHandler inputHandler, ConsoleDisplay consoleDisplay)
		{
			_inputHandler = inputHandler;
			_consoleDisplay = consoleDisplay;
		}

		public void Reading()
		{
			while (isListening)
			{
				var key = Console.ReadKey().Key;
				_inputHandler.InputHandle(key);
			}
		}

		public void StopReading()
		{
			_consoleDisplay.StopDisplay();
			isListening = false;
		}
	}
}