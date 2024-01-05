namespace TetrisEngine.Input
{
	public interface IInputReader
	{
		delegate void InputHandler(ConsoleKey key);
		event InputHandler OnNewInput;
		void Reading();

		char GetKey();
	}
}
