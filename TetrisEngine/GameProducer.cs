using TetrisEngine.Figure;

using static TetrisEngine.Figure.AbstractFigure;

namespace TetrisEngine
{
	public class GameProducer
	{
		private const int Width = 18, Height = 12;

		private readonly Position _startPosition = new(6, 0);
		private bool isGameOver = false, isPause = false;
		private readonly int Delay;
		private AbstractFigure _figure;
		private FigureFactory _figureFactory;

		private Cell[][] gameField;

		public delegate void GameFieldChangeHandler(IReadOnlyCollection<IReadOnlyCollection<Cell>> cells);
		public event GameFieldChangeHandler OnGameFieldChanged;

		public GameProducer(int delay)
		{
			Delay = delay;
			_figureFactory = new FigureFactory();
			InitializeGameField();
		}

		private void InitializeGameField()
		{
			gameField = new Cell[Height][];
			for (int i = 0; i < Height; i++)
			{
				gameField[i] = new Cell[Width];
				for (int j = 0; j < gameField[i].Length; j++)
					gameField[i][j] = new Cell(false, System.Drawing.Color.Black);
			}
		}

		#region API

		public void Start()
		{
			ProduceGame();
		}


		public void Pause()
		{
			isPause = true;
		}
		public void Resume()
		{
			isPause = false;
		}

		public void MoveFigureLeft()
		{
			if (_figure.LeftPos == 0)
				return;

			for (int i = 0; i < _figure.segments.Length; i++)
			{
				_figure.segments[i].X--;
				_figure.LeftPos--;
			}
			OnGameFieldChanged(gameField);
		}

		public void MoveFigureRight()
		{
			if (_figure.RightPos == gameField.Length - 1)
				return;

			for (int i = 0; i < _figure.segments.Length; i++)
			{
				_figure.segments[i].X++;
				_figure.LeftPos++;
			}
			OnGameFieldChanged(gameField);
		}

		public void MoveFigureDown()
		{
			if (_figure.BottomPos == Height - 1)
				return;

			for (int i = 0; i < _figure.segments.Length; i++)
			{
				_figure.segments[i].Y--;
				_figure.BottomPos--;
			}
			OnGameFieldChanged(gameField);
		}

		public void RotateFigure()
		{
			//todo добавить проверку что повернуть фигуру вообще возможно
			_figure.Rotate();
			OnGameFieldChanged(gameField);
		}
		#endregion

		private void ProduceGame()
		{
			while (!isGameOver)
			{
				if (!isPause)
				{
					CreateNewFigure();
					while (CanMoveDown())
					{
						Thread.Sleep(Delay);
						MoveFigureDown();
					}
					break;
				}
			}
		}

		private void CreateNewFigure()
		{
			_figure = _figureFactory.GetFigure(_startPosition);
			OnGameFieldChanged(gameField);
		}

		private bool CanMoveDown()
		{
			for (int i = 0; i < _figure.segments.Length; i++)
				if (_figure.segments[i].Y == _figure.BottomPos)
					if (gameField[_figure.segments[i].X][_figure.segments[i].Y].Filled)
						return false;

			return true;
		}

		private void DeleteRow()
		{
			for (int i = 0; i < gameField.GetLength(0); i++)
				gameField[gameField.Length - 1][i].Filled = false;

			MoveFilledCellsDown();
		}

		private void MoveFilledCellsDown()
		{
			for (int i = 0; i < gameField.Length; i++)
				for (int j = 1; j < gameField.GetLength(0) || gameField[i][j - 1].Filled == false; j++)
				{
					gameField[i][j].Filled = false;
				}
		}
	}
}