namespace TetrisEngine.Figure
{
	public abstract class AbstractFigure
	{
		public int LeftPos, TopPos, BottomPos, RightPos;

		public abstract void Rotate();
	}
}
