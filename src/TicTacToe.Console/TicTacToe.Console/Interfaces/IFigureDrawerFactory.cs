using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Interfaces
{
    public interface IFigureDrawerFactory
    {
        IFigureDrawer CreateFigureDrawer(FigureType figureType);
    }
}