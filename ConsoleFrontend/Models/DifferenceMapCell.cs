using TetrisEngine.Models;

namespace ConsoleFrontend.Models
{
    internal readonly record struct DifferenceMapCell(Cell Cell, bool IsDifference, int Top = 0, int Left = 0);
}
