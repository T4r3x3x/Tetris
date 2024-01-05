using ConsoleFrontend.Display;
using ConsoleFrontend.Input;

using TetrisEngine;

const int Delay = 1000;


var display = new ConsoleDisplay();
var inputReader = new ConsoleInputController();
GameProducer gameProducer = new(Delay);
ConsoleKeyInfo key;
Task controller, game;

if ((key = Console.ReadKey()).Key == ConsoleKey.Spacebar)
{
	game = Task.Run(gameProducer.Start);
	controller = Task.Run(inputReader.Reading);
}