using System.Drawing;

namespace TetrisEngine.Models
{
    public class Cell : ICloneable
    {
        public Cell(bool filled, Color color)
        {
            IsFilled = filled;
            Color = color;
        }

        public bool IsFilled { get; set; }

        public Color Color { get; set; }

        public static Color DefaultColor => Color.Black;

        public object Clone() => new Cell(IsFilled, Color);
    }
}