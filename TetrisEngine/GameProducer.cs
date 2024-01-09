using TetrisEngine.Figure;

namespace TetrisEngine
{
	public class GameProducer
	{
		private const int Width = 18, Height = 12;

		private int _countOfErasedRows = 0;
		private readonly Position _startPosition = new(6, 0);
		private bool isGameOver = false, isPause = false;
		private readonly int Delay;
		private AbstractFigure _figure;
		private FigureFactory _figureFactory = new FigureFactory();

		private Cell[][] _gameField;

		public delegate void GameFieldChangeHandler(IReadOnlyCollection<IReadOnlyCollection<Cell>> cells);
		public event GameFieldChangeHandler OnGameFieldChanged;

		public GameProducer(int delay)
		{
			Delay = delay;
			InitializeGameField();
		}

		private void InitializeGameField()
		{
			_gameField = new Cell[Height][];
			for (int i = 0; i < Height; i++)
			{
				_gameField[i] = new Cell[Width];
				for (int j = 0; j < _gameField[i].Length; j++)
					_gameField[i][j] = new Cell(false, System.Drawing.Color.DarkRed);
			}
		}

		#region API

		public int Start()
		{
			return ProduceGame();
		}

		public void Pause()
		{
			isPause = true;
		}

		public void Resume()
		{
			isPause = false;
		}

		public void MoveFigureLeft()//проверка что слева нет ничего 
		{
			if (_figure.LeftPos == 0)
				return;

			if (!CanMove(_figure, MoveDirection.Left))
				return;

			MoveFigure(_figure, MoveDirection.Left);

			_figure.LeftPos--;
			_figure.RightPos--;
			OnGameFieldChanged(_gameField);
		}

		public void MoveFigureRight()//проверка что справа нет ничего
		{
			if (_figure.RightPos == Width - 1)
				return;

			if (!CanMove(_figure, MoveDirection.Right))
				return;

			MoveFigure(_figure, MoveDirection.Right);

			_figure.LeftPos++;
			_figure.RightPos++;
			OnGameFieldChanged(_gameField);
		}

		public void MoveFigureDown()
		{
			if (_figure.BottomPos == Height - 1)
				return;

			if (!CanMove(_figure, MoveDirection.Down))
				return;

			MoveFigure(_figure, MoveDirection.Down);

			_figure.BottomPos++;
			OnGameFieldChanged(_gameField);
		}

		public void RotateFigure()
		{
			//todo добавить проверку что повернуть фигуру вообще возможно
			EraseFigure();
			_figure.Rotate(RotateDirection.right);
			if (!CanPutFigure())
			{
				_figure.Rotate(RotateDirection.left);
				return;
			}

			AddSegmentsToGameField(_figure.Segments);
			OnGameFieldChanged(_gameField);
		}

		#endregion

		private int ProduceGame()
		{
			while (!isGameOver)//можно заменить на тру ничего не поменяется
			{
				if (!isPause)
				{
					CreateNewFigure();
					if (!TryPutNewFigure())
						isGameOver = true;

					while (CanMoveDown())
					{
						Thread.Sleep(Delay);
						//	MoveFigureDown();
					}
					EraseFilledRows();
				}
			}
			return _countOfErasedRows;
		}


		private void EraseFigure()
		{
			foreach (var segmemt in _figure.Segments)
				_gameField[segmemt.Y][segmemt.X].Filled = false;
		}
		private bool CanRotateFigure()
		{
			return CanPutFigure();
		}

		private bool CanPutFigure()
		{
			foreach (var segment in _figure.Segments)
				if (_gameField[segment.Y][segment.X].Filled)
					return false;

			return true;
		}

		private bool CanMove(AbstractFigure figure, MoveDirection direction)
		{
			var moveDirection = GetMoveVector(direction);

			for (int i = 0; i < figure.Segments.Length; i++)
			{
				var cellPosition = figure.Segments[i] + moveDirection;
				var cell = _gameField[cellPosition.Y][cellPosition.X];

				if (cell.Filled && !figure.BelongToFigure(cellPosition))
					return false;
			}

			return true;
		}

		private void MoveFigure(AbstractFigure figure, MoveDirection direction)
		{
			var moveDirection = GetMoveVector(direction);

			for (int i = 0; i < figure.Segments.Length; i++)
			{
				var oldPosition = figure.Segments[i];
				figure.Segments[i] += moveDirection;
				_gameField[figure.Segments[i].Y][figure.Segments[i].X].Filled = true;
				if (!figure.BelongToFigure(oldPosition))
					_gameField[oldPosition.Y][oldPosition.X].Filled = false;
			}
		}

		private Position GetMoveVector(MoveDirection direction)
		{
			return direction switch
			{
				MoveDirection.Left => new Position(-1, 0),
				MoveDirection.Right => new Position(1, 0),
				MoveDirection.Down => new Position(0, 1),
			};
		}

		private void CreateNewFigure()
		{
			_figure = _figureFactory.GetFigure(_startPosition);
		}

		private bool TryPutNewFigure()
		{
			if (!CanPutFigure())
				return false;

			AddSegmentsToGameField(_figure.Segments);
			OnGameFieldChanged(_gameField);

			return true;
		}

		private void AddSegmentsToGameField(Position[] segmentsPosition)
		{
			foreach (var pos in segmentsPosition)
			{
				var i = pos.X;
				var j = pos.Y;
				_gameField[j][i].Filled = true;
			}
		}

		private bool CanMoveDown()
		{
			if (_figure.BottomPos == Height - 1)
				return false;

			for (int i = 0; i < _figure.Segments.Length; i++)
				if (_figure.Segments[i].Y == _figure.BottomPos)
					if (_gameField[_figure.Segments[i].Y + 1][_figure.Segments[i].X].Filled)
						return false;

			return true;
		}

		private void EraseFilledRows()
		{
			for (int i = Height - 1; i > 0;)
			{
				if (NeedToDelete(_gameField[i]))
				{
					DeleteRow(i);
					_countOfErasedRows++;
				}
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
				_gameField[rowNumber][i].Filled = false;

			MoveFilledCellsDown(rowNumber);
		}

		private void MoveFilledCellsDown(int startRow)
		{
			for (int i = 0; i < Width; i++)
				for (int j = startRow; j > 0 && _gameField[j - 1][i].Filled != false; j--)
				{
					_gameField[j][i].Filled = true;
					_gameField[j - 1][i].Filled = false;
				}
		}

	}

	enum MoveDirection
	{
		Left, Right, Down
	}

}