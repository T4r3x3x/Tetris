using ConsoleFrontend.Display;
using ConsoleFrontend.Input;

using TetrisEngine;

const int Delay = 400;

var display = new ConsoleDisplay();
GameProducer gameProducer = new(Delay);
var inputHandler = new DefaultInputHandler(gameProducer, display);
var inputReader = new ConsoleInputController(inputHandler);
ConsoleKey key = ConsoleKey.None;

gameProducer.OnGameFieldChanged += display.Display;

while (key != ConsoleKey.Spacebar)
{
	Console.Clear();
	Console.WriteLine("Press space to start game");
	key = Console.ReadKey().Key;
}

Task<int> game = Task.Run(gameProducer.Start);
Task controller = Task.Run(inputReader.Reading); //Убить поток 

game.Wait();
var countOfErasedRows = game.Result;
Console.Clear();
Console.WriteLine(string.Format("Game over! You have erased {0} rows!", countOfErasedRows));
Console.Read();