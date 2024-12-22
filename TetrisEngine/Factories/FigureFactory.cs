using System.Diagnostics;

using TetrisEngine.Enums;
using TetrisEngine.Extensions;
using TetrisEngine.Models;
using TetrisEngine.Models.Figure.Models.Abstract;
using TetrisEngine.Models.Figure.Models.Concreate;

namespace TetrisEngine.Factories
{
    internal partial class FigureFactory
    {
        private readonly Random _random = new Random();

        public AbstractFigure GetFigure(Position startPosition)
        {
            var figure = _random.NextEnumValue<EFiguries>();

            return figure switch
            {
                EFiguries.I => new LineFigure(startPosition),
                EFiguries.J => new JFigure(startPosition),
                EFiguries.L => new LFigure(startPosition),
                EFiguries.O => new OFigure(startPosition),
                EFiguries.S => new SFigure(startPosition),
                EFiguries.T => new TFigure(startPosition),
                EFiguries.Z => new ZFigure(startPosition),
                _ => throw new UnreachableException($"There are not expected value {figure} of {nameof(EFiguries)}"),
            };
        }
    }
}
