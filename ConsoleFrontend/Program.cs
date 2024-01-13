using ConsoleFrontend.Display;
using ConsoleFrontend.Input;

using TetrisEngine;

const int Delay = 1000;

var display = new ConsoleDisplay();
GameProducer gameProducer = new(Delay);
var inputHandler = new DefaultInputHandler(gameProducer, display);
var inputReader = new ConsoleInputController(inputHandler);
gameProducer.OnGameFieldChanged += display.Update;


WaitUntilPressedStart();

Task<int> game = Task.Run(gameProducer.Start);
Task controller = Task.Run(inputReader.Reading);

game.Wait();
var countOfErasedRows = game.Result;

inputReader.StopReading();
controller.Wait();

Console.Clear();
Console.WriteLine(string.Format("Game over! You have erased {0} rows!", countOfErasedRows));
Console.Read();

void WaitUntilPressedStart()
{
	ConsoleKey key = ConsoleKey.None;
	while (key != ConsoleKey.Spacebar)
	{
		Console.Clear();
		Console.WriteLine("Press space to start game");
		key = Console.ReadKey(true).Key;
	}
}