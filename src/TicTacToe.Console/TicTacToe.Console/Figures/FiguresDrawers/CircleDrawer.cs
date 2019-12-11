using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public class CircleDrawer : FigureDrawer, IFigureDrawer
    {
        public CircleDrawer(IConsole console, FigureType figureType) 
            : base(console, figureType)
        {

        }


        public override void DrawFigure()
        {
            Console.Write("o");
        }
    }
}