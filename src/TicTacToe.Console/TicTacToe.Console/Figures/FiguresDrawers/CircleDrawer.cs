using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public class CircleDrawer : FigureDrawer, IFigureDrawer
    {
        private readonly IConsole _console;


        public CircleDrawer(IConsole console, FigureType figureType) 
            : base(figureType)
        {
            _console = console;
        }


        protected override void Draw(IFigure figure)
        {
            _console.Write("o");
        }
    }
}