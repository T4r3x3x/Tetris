using System.Drawing;

using TetrisEngine.Models.Figure.Models.Abstract;

namespace TetrisEngine.Models.Figure.Models.Concreate
{
    internal class SFigure : AbstractFigure
    {
        public SFigure(Position startPosition) : base(startPosition) { }

        public override Color Color => Color.Green;

        protected override Position[] _segmentsLocalPosition => [new(0, 0), new(1, 0), new(-1, 1), new(0, 1)];

        public override Position[] GetRotationDisplacement()
        {
            return _rotateState switch
            {
                0 => [new(0, 0), new(-1, 1), new(0, -2), new(-1, -1)],
                1 => [new(0, 0), new(-1, -1), new(2, 0), new(1, -1)],
                2 => [new(0, 0), new(1, -1), new(0, 2), new(1, 1)],
                3 => [new(0, 0), new(1, 1), new(-2, 0), new(-1, 1)],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
