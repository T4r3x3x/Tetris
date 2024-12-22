using ConsoleFrontend.Models;

using TetrisEngine.Models;

using static ConsoleFrontend.Colors.ColorConverter;

namespace ConsoleFrontend.Display
{
    public class ConsoleDisplay
    {
        private const int GameFieldWidth = 10, GameFieldHeight = 20, VerticalLineWidth = 1, VerticalLinesCount = 2;

        private const char FilledCellSymbol = '#', EmptyCellSymbol = ' ';

        private Cell[][] _gameFieldLastFrame = Array.Empty<Cell[]>();
        private object _sync = new object();


        private void PrintEmptyGameField()
        {
            Console.Clear();
            var lineWidth = GameFieldWidth + VerticalLineWidth * VerticalLinesCount;
            PrintHorizontalLine(lineWidth);
            for (int i = 0; i < GameFieldHeight; i++)
            {
                Console.Write("|");
                for (int j = 0; j < GameFieldWidth; j++)
                    Console.Write(" ");

                Console.Write("|\n");
            }
            PrintHorizontalLine(lineWidth);
        }

        private void PrintHorizontalLine(int lineWidth)
        {
            for (int i = 0; i < lineWidth; i++)
                Console.Write('-');
            Console.WriteLine();
        }

        public void Update(Cell[][] gameField)
        {
            lock (_sync)
            {
                var differenceMap = GetFramesDifference(gameField);
                if (_gameFieldLastFrame is null)
                    PrintEmptyGameField();
                _gameFieldLastFrame = gameField;
                RedisplayGameField(differenceMap, differenceMap);
            }
        }

        private List<List<bool>> GetFramesDifference(Cell[][] gameFieldCurrentFrame)
        {
            List<List<bool>> differenceMap = new List<List<bool>>();

            if (_gameFieldLastFrame == null)
            {
                differenceMap = gameFieldCurrentFrame.Select(row => row.Select(cell => cell.IsFilled | true).ToList()).ToList();
                return differenceMap;
            }

            differenceMap = gameFieldCurrentFrame.Select(row => row.Select(cell => cell.IsFilled).ToList()).ToList();
            var lastFrameFill = _gameFieldLastFrame.Select(row => row.Select(cell => cell.IsFilled).ToList()).ToList();

            for (int i = 0; i < differenceMap.Count; i++)
                for (int j = 0; j < differenceMap[0].Count; j++)
                    differenceMap[i][j] = differenceMap[i][j] != lastFrameFill[i][j];

            return differenceMap;
        }

        private void RedisplayGameField(DifferenceMap differenceMap, Cell[][] gameField)
        {
            lock (_sync)
            {
                foreach (var cell in differenceMap)
                    if (cell.IsDifference)
                        RedisplayCell(cell.Cell, cell.Top, cell.Left);

                Console.SetCursorPosition(0, GameFieldHeight + 1);
            }
        }

        private void RedisplayCell(Cell cell, int top, int left)
        {
            Console.SetCursorPosition(left + 1, top + 1);

            Console.ForegroundColor = ClosestConsoleColor(cell.Color);
            var symbol = cell.IsFilled ? FilledCellSymbol : EmptyCellSymbol;
            Console.Write(symbol);

            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}