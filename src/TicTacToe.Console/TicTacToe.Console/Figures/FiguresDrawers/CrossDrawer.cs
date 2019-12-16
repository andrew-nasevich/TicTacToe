using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public class CrossDrawer : FigureDrawer
    {
        private readonly IConsole _console;


        public CrossDrawer(IConsole console) 
            : base(FigureType.Cross)
        {
            _console = console;
        }


        protected override void Draw(IFigure figure)
        {
            _console.Write("x");
        }
    }
}