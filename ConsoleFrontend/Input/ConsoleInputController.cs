namespace ConsoleFrontend.Input
{
	public class ConsoleInputController
	{
		private const int InputReadDelay = 100;
		private readonly IInputHandler _inputHandler;
		private bool isListening = true;

		public ConsoleInputController(IInputHandler inputHandler)
		{
			_inputHandler = inputHandler;
		}

		public void Reading()
		{
			while (isListening)
			{
				if (Console.KeyAvailable)
				{
					var key = Console.ReadKey(true).Key;
					_inputHandler.InputHandle(key);
					//	Thread.Sleep(InputReadDelay);
				}
			}
		}

		public void StopReading()
		{
			isListening = false;
		}
	}
}