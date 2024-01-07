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

			for (int i = 0; i < Width; i++)
			{
				gameField[Height - 1][i].Filled = true;
				gameField[Height - 2][i].Filled = true;
				gameField[Height - 3][2].Filled = true;
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
				var oldPos = _figure.segments[i];
				_figure.segments[i].X--;
				var newPos = _figure.segments[i];
				gameField[newPos.Y][newPos.X].Filled = true;
				gameField[oldPos.Y][oldPos.X].Filled = false;
			}

			_figure.LeftPos--;
			_figure.RightPos--;
			OnGameFieldChanged(gameField);
		}

		public void MoveFigureRight()
		{
			if (_figure.RightPos == Width - 1)
				return;

			for (int i = _figure.segments.Length - 1; i > -1; i--) //в таком порядке, чтобы сначала обрабатывался правый сегмент, а потом левый
			{
				var oldPos = _figure.segments[i];
				_figure.segments[i].X++;
				var newPos = _figure.segments[i];
				gameField[newPos.Y][newPos.X].Filled = true;
				gameField[oldPos.Y][oldPos.X].Filled = false;
			}

			_figure.LeftPos++;
			_figure.RightPos++;
			OnGameFieldChanged(gameField);
		}

		public void MoveFigureDown()
		{
			if (!CanMoveDown())
				return;

			for (int i = _figure.segments.Length - 1; i > -1; i--) //в таком порядке, чтобы сначала обрабатывались нижние сегменты, а потом верхнии
			{
				var oldPos = _figure.segments[i];
				_figure.segments[i].Y++;
				var newPos = _figure.segments[i];
				gameField[newPos.Y][newPos.X].Filled = true;
				gameField[oldPos.Y][oldPos.X].Filled = false;
			}

			_figure.BottomPos++;
			OnGameFieldChanged(gameField);
		}

		public void RotateFigure()
		{
			//todo добавить проверку что повернуть фигуру вообще возможно
			_figure.Rotate();
			OnGameFieldChanged(gameField);
		}
		#endregion

		private void ProduceGame()//todo Добавить проверку что новую фигуру можно разместить
		{
			while (!isGameOver)
			{
				if (!isPause)
				{
					CreateNewFigure();
					while (CanMoveDown())
					{
						Thread.Sleep(Delay);
						//MoveFigureDown();
					}
					DeleteFilledRows();
				}
			}
		}

		private void CreateNewFigure()
		{
			_figure = _figureFactory.GetFigure(_startPosition);
			AddSegmentsToGameField(_figure.segments);
			OnGameFieldChanged(gameField);
		}

		private void AddSegmentsToGameField(Position[] segmentsPosition)
		{
			foreach (var pos in segmentsPosition)
			{
				var i = pos.X;
				var j = pos.Y;
				gameField[j][i].Filled = true;
			}
		}

		private bool CanMoveDown()//todo баг 
		{
			if (_figure.BottomPos == Height - 1)
				return false;

			for (int i = 0; i < _figure.segments.Length; i++)
				if (_figure.segments[i].Y == _figure.BottomPos)
					if (gameField[_figure.segments[i].Y + 1][_figure.segments[i].X].Filled)
						return false;

			return true;
		}

		private void DeleteFilledRows()
		{
			for (int i = Height - 1; i > 0;)
			{
				if (NeedToDelete(gameField[i]))
					DeleteRow(i);
				else
					i--;
			}
		}

		private bool NeedToDelete(Cell[] row)
		{
			for (int i = 0; i < row.Length; i++)
				if (row[i].Filled == false)
					return false;

			return true;
		}

		private void DeleteRow(int rowNumber)
		{
			for (int i = 0; i < Width; i++)
				gameField[rowNumber][i].Filled = false;

			MoveFilledCellsDown(rowNumber);
		}

		private void MoveFilledCellsDown(int startRow)
		{
			for (int i = 0; i < Width; i++)
				for (int j = startRow; j > 0 && gameField[j - 1][i].Filled != false; j--)
				{
					gameField[j][i].Filled = true;
					gameField[j - 1][i].Filled = false;
				}
		}
	}
}