namespace Tetris.Figuries
{
	public abstract class AbstractFigure
	{
		public int LeftPos, TopPos, BottomPos, RightPos;


		public void Move(Direction direction)
		{

		}

		public abstract void Rotate();
	}
}
