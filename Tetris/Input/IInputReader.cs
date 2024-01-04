namespace Tetris.Input
{
	internal interface IInputReader
	{
		delegate char GetInput();
		event GetInput OnNewInput;
		void Reading();
	}
}
