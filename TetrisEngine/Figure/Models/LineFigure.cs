﻿using System.Drawing;

namespace TetrisEngine.Figure.Models
{
	internal class LineFigure : AbstractFigure
	{
		public LineFigure(Position startPosition) : base(startPosition) { }

		public override Color Color => Color.Turquoise;

		protected override Position[] _segmentsLocalPosition => [new(0, 0), new(1, 0), new(2, 0), new(3, 0)];

		public override Position[] GetRotationDisplacement()
		{
			return (_rotateState % 2) switch
			{
				0 => [new Position(1, -1), new Position(0, 0), new Position(-1, 1), new Position(-2, 2)],
				1 => [new Position(-1, 1), new Position(0, 0), new Position(1, -1), new Position(2, -2)],
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}