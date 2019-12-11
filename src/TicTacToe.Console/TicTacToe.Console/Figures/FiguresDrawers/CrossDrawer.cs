using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public class CrossDrawer : FigureDrawer, IFigureDrawer
    {
        public CrossDrawer(IConsole console, FigureType figureType) 
            : base(console, figureType)
        {

        }


        public override void DrawFigure()
        {
            Console.Write("x");
        }
    }
}