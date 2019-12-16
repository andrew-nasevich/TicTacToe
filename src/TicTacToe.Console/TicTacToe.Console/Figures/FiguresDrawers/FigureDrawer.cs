using System;
using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Figures.FiguresDrawers
{
    public abstract class FigureDrawer : IFigureDrawer
    {
        public FigureType FigureType { get; }
        
        
        protected FigureDrawer(FigureType figureType)
        {
            FigureType = figureType;
        }


        public void DrawFigure(IFigure figure)
        {
            if (figure.Type != FigureType)
            {
                throw new InvalidOperationException($"The type of the received figure should be: {FigureType}");
            }
            Draw(figure);
        }


        protected abstract void Draw(IFigure figure);
    }
}