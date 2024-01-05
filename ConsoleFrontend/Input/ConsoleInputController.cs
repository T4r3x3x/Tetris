namespace ConsoleFrontend.Input
{
	public class ConsoleInputController
	{
		private readonly IInputHandler _inputHandler;

		public ConsoleInputController(IInputHandler inputHandler)
		{
			_inputHandler = inputHandler;
		}

		public void Reading()
		{
			while (true)
			{
				var key = Console.ReadKey().Key;
				_inputHandler.InputHandle(key);
			}
		}
	}
}