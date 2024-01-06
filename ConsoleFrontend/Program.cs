using ConsoleFrontend.Display;
using ConsoleFrontend.Input;

using TetrisEngine;

const int Delay = 1000;


var display = new ConsoleDisplay();
GameProducer gameProducer = new(Delay);
var inputHandler = new DefaultInputHandler(gameProducer);
var inputReader = new ConsoleInputController(inputHandler);
ConsoleKeyInfo key;
Task controller, game;
gameProducer.OnGameFieldChanged += display.Display;

Console.WriteLine("Press space to start game");
if ((key = Console.ReadKey()).Key == ConsoleKey.Spacebar)
{
	game = Task.Run(gameProducer.Start);
	controller = Task.Run(inputReader.Reading);
	game.Wait();
}

//Console.Read();