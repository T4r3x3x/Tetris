namespace ConsoleFrontend.Input
{
    public class ConsoleInputController
    {
        private readonly IInputHandler _inputHandler;
        private bool isListening = true;

        public ConsoleInputController(IInputHandler inputHandler) => _inputHandler = inputHandler;

        public void Reading()
        {
            while (isListening)
            {
                if (!Console.KeyAvailable)
                    continue;

                var key = Console.ReadKey(true).Key;
                _inputHandler.InputHandle(key);
            }
        }

        public void StopReading() => isListening = false;
    }
}