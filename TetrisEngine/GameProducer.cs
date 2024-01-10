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

		public void MoveFigureLeft()
		{
			if (!CanMove(_figure, MoveDirection.Left))
				return;

			MoveFigure(_figure, MoveDirection.Left);
			OnGameFieldChanged(_gameField);
		}

		public void MoveFigureRight()
		{
			if (!CanMove(_figure, MoveDirection.Right))
				return;

			MoveFigure(_figure, MoveDirection.Right);
			OnGameFieldChanged(_gameField);
		}

		public void MoveFigureDown()
		{
			if (!CanMove(_figure, MoveDirection.Down))
				return;

			MoveFigure(_figure, MoveDirection.Down);
			OnGameFieldChanged(_gameField);
		}

		public void RotateFigure()
		{
			if (!CanRotateFigure())//Обобщить canRotate и canMove
				return;

			EraseFigure();
			_figure.Rotate();
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

					while (CanMove(_figure, MoveDirection.Down))
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
			var displacement = _figure.GetRotateDisplacement();
			for (int i = 0; i < _figure.Segments.Length; i++)
			{
				var cellPosition = _figure.Segments[i] + displacement[i];
				if (!IsSegmentBelongToGameField(cellPosition))
					return false;

				var cell = _gameField[cellPosition.Y][cellPosition.X];
				if (cell.Filled && !_figure.BelongToFigure(cellPosition))
					return false;
			}

			return true;
		}


		private bool CanPutFigure()
		{
			foreach (var segment in _figure.Segments)
				if (!IsSegmentBelongToGameField(segment) || _gameField[segment.Y][segment.X].Filled)
					return false;

			return true;
		}

		private bool CanMove(AbstractFigure figure, MoveDirection direction)
		{
			var moveDirection = GetMoveVector(direction);

			for (int i = 0; i < figure.Segments.Length; i++)
			{
				var cellPosition = figure.Segments[i] + moveDirection;
				if (!IsSegmentBelongToGameField(cellPosition))
					return false;

				var cell = _gameField[cellPosition.Y][cellPosition.X];
				if (cell.Filled && !figure.BelongToFigure(cellPosition))
					return false;
			}

			return true;
		}

		private bool IsSegmentBelongToGameField(Position segment)
		{
			if (-1 < segment.X && segment.X < Width && -1 < segment.Y && segment.Y < Height)
				return true;

			return false;
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

		private void EraseFilledRows()
		{
			for (int i = Height - 1; i > 0;)
			{
				if (NeedToDelete(_gameField[i]))
				{
					EraseRow(i);
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

		private void EraseRow(int rowNumber)
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