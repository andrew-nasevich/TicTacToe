using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Interfaces
{
    public interface IBoardDrawer
    {
        void DrawBoard(IBoard board);
    }
}