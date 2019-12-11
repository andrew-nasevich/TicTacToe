using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public abstract class FigureDrawer : IFigureDrawer
    {
        protected readonly IConsole Console;


        public FigureType FigureType { get; }
        
        
        protected FigureDrawer(IConsole console, FigureType figureType)
        {
            Console = console;
            FigureType = figureType;
        }

        
        public abstract void DrawFigure();
    }
}