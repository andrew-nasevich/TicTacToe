using System.Collections.Generic;
using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;

namespace TicTacToe.Console.Figures
{
    public class FigureDrawerProvider : IFigureDrawerProvider
    {
        private readonly IDictionary<FigureType, IFigureDrawer> _figureDrawers;


        public FigureDrawerProvider(params IFigureDrawer[] figureDrawers)
        {
            _figureDrawers = new Dictionary<FigureType, IFigureDrawer>();

            foreach (var figureDrawer in figureDrawers)
            {
                _figureDrawers.Add(figureDrawer.FigureType, figureDrawer);
            }
        }


        public IFigureDrawer GetFigureDrawer(FigureType figureType)
        {
            return _figureDrawers.TryGetValue(figureType, out var figureDrawer) ? figureDrawer : null;
        }
    }
}