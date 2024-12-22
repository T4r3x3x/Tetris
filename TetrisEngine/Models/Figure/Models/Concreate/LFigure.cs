using System.Drawing;

using TetrisEngine.Models.Figure.Models.Abstract;

namespace TetrisEngine.Models.Figure.Models.Concreate
{
    internal class LFigure : AbstractFigure
    {
        public LFigure(Position startPosition) : base(startPosition) { }

        public override Color Color => Color.SandyBrown;

        protected override Position[] _segmentsLocalPosition => [new(0, 0), new(0, 1), new(0, 2), new(1, 2)];
        public override Position[] GetRotationDisplacement()
        {
            return _rotateState switch
            {
                0 => [new(1, 1), new(0, 0), new(-1, -1), new(-2, 0)],
                1 => [new(-1, 1), new(0, 0), new(1, -1), new(0, -2)],
                2 => [new(-1, -1), new(0, 0), new(1, 1), new(2, 0)],
                3 => [new(1, -1), new(0, 0), new(-1, 1), new(0, 2)],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
