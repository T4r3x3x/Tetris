using ConsoleFrontend.Display;
using ConsoleFrontend.Input;

using TetrisEngine;

const int Delay = 1000;


var display = new ConsoleDisplay();
GameProducer gameProducer = new(Delay);
var inputHandler = new DefaultInputHandler(gameProducer, display);
var inputReader = new ConsoleInputController(inputHandler);
ConsoleKey key = ConsoleKey.N;
Task controller, game;
gameProducer.OnGameFieldChanged += display.Display;

while (key != ConsoleKey.Spacebar)
{
	Console.Clear();
	Console.WriteLine("Press space to start game");
	key = Console.ReadKey().Key;
}

game = Task.Run(gameProducer.Start);
controller = Task.Run(inputReader.Reading);
game.Wait();
//Console.Read();