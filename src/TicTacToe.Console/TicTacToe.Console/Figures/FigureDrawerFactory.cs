using System;
using TicTacToe.Console.Figures.FiguresDrawers;
using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Figures
{
    public class FigureDrawerFactory : IFigureDrawerFactory
    {
        private readonly IConsole _console;


        public FigureDrawerFactory(IConsole console)
        {
            _console = console;
        }


        public IFigureDrawer CreateFigureDrawer(FigureType figureType)
        {
            switch (figureType)
            {
                case FigureType.Circle:
                    return new CircleDrawer(_console, figureType);
                case FigureType.Cross:
                    return new CrossDrawer(_console, figureType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(figureType), figureType, "Unknown figureType value");
            }
        }
    }
}