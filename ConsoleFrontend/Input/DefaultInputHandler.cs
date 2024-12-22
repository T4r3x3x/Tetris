using TetrisEngine;

namespace ConsoleFrontend.Input
{
    public class DefaultInputHandler : IInputHandler
    {
        private readonly GameProducer _game;

        public DefaultInputHandler(GameProducer game)
        {
            _game = game;
        }

        public void InputHandle(ConsoleKey key)
        {
            Action? action = key switch
            {
                ConsoleKey.A => _game.MoveFigureLeft,
                ConsoleKey.D => _game.MoveFigureRight,
                ConsoleKey.W => _game.RotateFigure,
                ConsoleKey.S => _game.MoveFigureDown,
                ConsoleKey.Escape => _game.Pause,
                ConsoleKey.Enter => _game.Resume,
                _ => null!
            };
            action?.Invoke();
        }
    }
}
