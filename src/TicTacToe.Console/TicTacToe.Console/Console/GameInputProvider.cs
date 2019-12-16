using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Console
{
    public class GameInputProvider : IGameInputProvider
    {
        private readonly IConsole _console;


        public GameInputProvider(IConsole console)
        {
            _console = console;
        }


        public void GetNextCellPosition(out int row, out int column, IPlayer player)
        {
            _console.WriteLine($"{player.Name}, please, write your next figure position");
            row = _console.ReadInt("Please, write row: ") - 1;
            column = _console.ReadInt("Please, write column: ") - 1;
        }
    }
}