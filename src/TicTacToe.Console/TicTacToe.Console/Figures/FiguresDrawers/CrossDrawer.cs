using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public class CrossDrawer : FigureDrawer, IFigureDrawer
    {
        private readonly IConsole _console;


        public CrossDrawer(IConsole console, FigureType figureType) 
            : base(figureType)
        {
            _console = console;
        }


        protected override void Draw(IFigure figure)
        {
            base.DrawFigure(figure);

            _console.Write("x");
        }
    }
}