using System.Linq;
using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Boards
{
    public class BoardDrawer : IBoardDrawer
    {
        private readonly IFigureDrawerProvider _figureDrawerProvider;
        private readonly IConsole _console;
        

        public BoardDrawer(IFigureDrawerProvider figureDrawerProvider, IConsole console)
        {
            _figureDrawerProvider = figureDrawerProvider;
            _console = console;
        }


        public void DrawBoard(IBoard board)
        {
            DrawBoardBorder(board);
            for (var row = 0; row < board.BoardSize; row++)
            {
                DrawBoardRow(board, row);
                DrawBoardBorder(board);
            }
        }


        private void DrawBoardBorder(IBoard board)
        {
            _console.WriteLine("+" + Enumerable.Repeat("-+", board.BoardSize));
        }

        private void DrawBoardRow(IBoard board, int row)
        {
            _console.Write("|");

            var cells = board.Where(c => c.Row == row).ToList();
            foreach (var cell in cells)
            {
                if (cell.IsEmpty)
                {
                    _console.Write(" ");
                }
                else
                {
                    var figureDrawer = _figureDrawerProvider.GetFigureDrawer(cell.Figure.Type);
                    figureDrawer.DrawFigure(cell.Figure);
                }
                _console.Write("|");
            }

            _console.WriteLine();
        }
    }
}