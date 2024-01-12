using TetrisEngine.Figure;

namespace TetrisEngine
{
	public class GameProducer
	{
		private const int Width = 10, Height = 20;
		private const double DelayDecrisingPercent = 0.01;

		private int _countOfErasedRows = 0;
		private int _delay;

		private readonly Position _startPosition = new(6, 0);
		private bool isGameOver = false, isPause = false;

		private AbstractFigure _figure;
		private FigureFactory _figureFactory = new FigureFactory();

		private Cell[][] _gameField;

		public delegate void GameFieldChangeHandler(Cell[][] cells);//передавать копию!!!!!!!
		public event GameFieldChangeHandler OnGameFieldChanged;

		public GameProducer(int startDelay)
		{
			_delay = startDelay;
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

		public void MoveFigureLeft() => MoveFigure(MoveDirection.Left);

		public void MoveFigureRight() => MoveFigure(MoveDirection.Right);

		public void MoveFigureDown() => MoveFigure(MoveDirection.Down);

		public void RotateFigure()
		{
			if (!CanRotateFigure())//Обобщить canRotate и canMove
				return;

			Position[] OldSegmentsPosition = new Position[_figure.SegmentsPosition.Count()];
			_figure.SegmentsPosition.ToList().CopyTo(OldSegmentsPosition, 0);

			_figure.Rotate();
			RefillGameField(OldSegmentsPosition);

			OnGameFieldChanged(_gameField);
		}
		#endregion

		private int ProduceGame()
		{
			while (TryPutNewFigure())
			{
				if (!isPause)
				{
					while (CanMove(MoveDirection.Down))
					{
						Thread.Sleep(_delay);
						//	MoveFigureDown();
					}
					var erasedRowsOnThisIter = EraseFilledRows();
					IncreaseGameSpeed(erasedRowsOnThisIter);
				}
			}
			isGameOver = true;
			return _countOfErasedRows;
		}

		private void EraseFigure()
		{
			foreach (var segmemt in _figure.SegmentsPosition)
				_gameField[segmemt.Y][segmemt.X].IsFilled = false;
		}

		private bool CanRotateFigure()
		{
			var displacement = _figure.GetRotationDisplacement();
			var segmentsPosition = _figure.SegmentsPosition.ToArray();

			Position[] newSegmentsPosition = new Position[_figure.SegmentsPosition.Count()];

			for (int i = 0; i < segmentsPosition.Length; i++)
				newSegmentsPosition[i] = segmentsPosition[i] + displacement[i];

			return CanExecuteMovement(newSegmentsPosition);
		}

		private bool CanMove(MoveDirection direction)
		{
			var moveDirection = GetMoveVector(direction);
			var segmentsPosition = _figure.SegmentsPosition.ToArray();

			Position[] newSegmentsPosition = new Position[_figure.SegmentsPosition.Count()];

			for (int i = 0; i < segmentsPosition.Length; i++)
				newSegmentsPosition[i] = segmentsPosition[i] + moveDirection;

			return CanExecuteMovement(newSegmentsPosition);
		}

		private bool CanExecuteMovement(Position[] newSegmentsPosition)
		{
			for (int i = 0; i < newSegmentsPosition.Length; i++)
			{
				if (!IsSegmentBelongToGameField(newSegmentsPosition[i]))
					return false;

				var cell = _gameField[newSegmentsPosition[i].Y][newSegmentsPosition[i].X];
				if (cell.IsFilled && !_figure.IsBelong(newSegmentsPosition[i]))
					return false;
			}

			return true;
		}



		private void MoveFigure(MoveDirection direction)//название похоже на MoveFigure изменить.
		{
			if (!CanMove(direction))
				return;

			var moveDirection = GetMoveVector(direction);
			Position[] OldSegmentsPosition = new Position[_figure.SegmentsPosition.Count()];
			_figure.SegmentsPosition.ToList().CopyTo(OldSegmentsPosition, 0);

			_figure.Move(moveDirection);
			RefillGameField(OldSegmentsPosition.ToArray());

			OnGameFieldChanged(_gameField);
		}

		private void RefillGameField(IEnumerable<Position> figureOldSegmentsPosition)
		{
			foreach (var segment in _figure.SegmentsPosition)
				_gameField[segment.Y][segment.X].IsFilled = true;

			foreach (var segment in figureOldSegmentsPosition)
				if (!_figure.IsBelong(segment))
					_gameField[segment.Y][segment.X].IsFilled = false;
		}

		private bool CanPutFigure()
		{
			foreach (var segment in _figure.SegmentsPosition)
				if (!IsSegmentBelongToGameField(segment) || _gameField[segment.Y][segment.X].IsFilled)
					return false;

			return true;
		}


		private bool IsSegmentBelongToGameField(Position segment) => (-1 < segment.X && segment.X < Width) && (-1 < segment.Y && segment.Y < Height);


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
			CreateNewFigure();

			if (!CanPutFigure())
				return false;

			PutFigure();
			OnGameFieldChanged(_gameField);

			return true;
		}

		private void PutFigure()
		{
			foreach (var pos in _figure.SegmentsPosition)
				_gameField[pos.Y][pos.X].IsFilled = true;
		}

		private void IncreaseGameSpeed(int incresingCount)
		{
			for (int i = 0; i < incresingCount; i++)
				_delay = Convert.ToInt32(_delay * (1 - DelayDecrisingPercent));
		}

		private int EraseFilledRows()
		{
			int countOfErasedRows = 0;

			for (int i = Height - 1; i > 0;)
			{
				if (NeedToDelete(_gameField[i]))
				{
					EraseRow(i);
					_countOfErasedRows++;
					countOfErasedRows++;
				}
				else
					i--;
			}
			return countOfErasedRows;
		}

		//
		//Метод IsEmpty || CanPut использовать для проверки передвижения?
		//

		private bool NeedToDelete(Cell[] row)
		{
			for (int i = 0; i < row.Length; i++)
				if (row[i].IsFilled == false)
					return false;

			return true;
		}

		private void EraseRow(int rowNumber)
		{
			for (int i = 0; i < Width; i++)
				_gameField[rowNumber][i].IsFilled = false;

			MoveFilledCellsDown(rowNumber);
		}

		private void MoveFilledCellsDown(int startRow)
		{
			for (int i = 0; i < Width; i++)
				for (int j = startRow; j > 0 && _gameField[j - 1][i].IsFilled != false; j--)
				{
					_gameField[j][i].IsFilled = true;
					_gameField[j - 1][i].IsFilled = false;
				}
		}

	}

	enum MoveDirection
	{
		Left, Right, Down
	}
}