using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Interfaces
{
    public interface IFigureDrawerProvider
    {
        IFigureDrawer GetFigureDrawer(FigureType figureType);
    }
}