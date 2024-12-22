using System.Drawing;

using TetrisEngine.Models.Figure.Models.Abstract;

namespace TetrisEngine.Models.Figure.Models.Concreate
{
    internal class ZFigure : AbstractFigure
    {
        public ZFigure(Position startPosition) : base(startPosition) { }

        public override Color Color => Color.Red;

        protected override Position[] _segmentsLocalPosition => [new(0, 0), new(-1, 0), new(0, 1), new(1, 1)];

        public override Position[] GetRotationDisplacement()
        {
            return _rotateState switch
            {
                0 => [new(0, 0), new(1, -1), new(-1, -1), new(-2, 0)],
                1 => [new(0, 0), new(1, 1), new(1, -1), new(0, -2)],
                2 => [new(0, 0), new(-1, 1), new(1, 1), new(2, 0)],
                3 => [new(0, 0), new(-1, -1), new(-1, 1), new(0, 2)],
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}