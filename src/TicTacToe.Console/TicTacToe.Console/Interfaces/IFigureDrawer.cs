using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Interfaces
{
    public interface IFigureDrawer
    {
        FigureType FigureType { get; }


        void DrawFigure(IFigure figure);
    }
}