using TetrisEngine;
using TetrisEngine.Display;
using TetrisEngine.Input;

const int Width = 3, Height = 3, Delay = 1000;

Cell[,] field = new Cell[Width, Height];
IDisplay display = new ConsoleDisplay();
IInputReader inputReader = new ConsoleInputReader();
GameProducer gameProducer = new(Delay, inputReader);
ConsoleKeyInfo key;
Task controller, game;
if ((key = Console.ReadKey()).Key == ConsoleKey.Spacebar)
{
	game = Task.Run(gameProducer.StartGame);
	controller = Task.Run(inputReader.Reading);
}