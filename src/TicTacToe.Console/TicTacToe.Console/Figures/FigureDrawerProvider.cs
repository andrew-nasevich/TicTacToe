using System.Collections.Generic;
using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Figures
{
    public class FigureDrawerProvider : IFigureDrawerProvider
    {
        private readonly IFigureDrawerFactory _figureDrawerFactory;

        private readonly IDictionary<FigureType, IFigureDrawer> _figureDrawers;
        

        public FigureDrawerProvider(IFigureDrawerFactory figureDrawerFactory)
        {
            _figureDrawerFactory = figureDrawerFactory;

            _figureDrawers = new Dictionary<FigureType, IFigureDrawer>();
        }


        public IFigureDrawer GetFigureDrawer(FigureType figureType)
        {
            if (!_figureDrawers.TryGetValue(figureType, out var figureDrawer))
            {
                figureDrawer = _figureDrawerFactory.CreateFigureDrawer(figureType);
                _figureDrawers.Add(figureDrawer.FigureType, figureDrawer);
            }

            return figureDrawer;
        }
    }
}