using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Interfaces
{
    public interface IFigureDrawer
    {
        FigureType FigureType { get; }


        void DrawFigure();
    }
}