﻿using TetrisEngine.Figure;
using TetrisEngine.Figure.Models;

namespace TetrisEngine
{
	internal class FigureFactory
	{
		private readonly Random _random = new Random();
		private const int FiguriesCount = 7;

		public AbstractFigure GetFigure(Position startPosition)
		{
			var figure = (Figuries)_random.Next(FiguriesCount);

			return figure switch
			{
				Figuries.I => new LineFigure(startPosition),
				Figuries.J => new JFigure(startPosition),
				Figuries.L => new LFigure(startPosition),
				Figuries.O => new OFigure(startPosition),
				Figuries.S => new SFigure(startPosition),
				Figuries.T => new TFigure(startPosition),
				Figuries.Z => new ZFigure(startPosition),
				_ => throw new Exception(),
			};
		}

		public enum Figuries
		{
			I,
			J,
			L,
			O,
			S,
			T,
			Z,
		}
	}
}
