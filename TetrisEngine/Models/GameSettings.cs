namespace TetrisEngine.Models
{
    public record GameSettings(int GameFieldWidth, int GameFieldHeight, int StartMoveFrequency, int ReduceFrequencyPercent);
}
