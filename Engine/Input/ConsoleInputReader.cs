namespace TetrisEngine.Input
{
	public class ConsoleInputReader : IInputReader
	{
		public event IInputReader.InputHandler OnNewInput;

		public char GetKey() => throw new NotImplementedException();
		public void Reading() => throw new NotImplementedException();
	}
}
