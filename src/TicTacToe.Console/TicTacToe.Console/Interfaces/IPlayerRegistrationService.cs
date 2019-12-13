using System.Collections.Generic;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Interfaces
{
    public interface IPlayerRegistrationService
    {
        IPlayer RegisterPlayer(IReadOnlyCollection<FigureType> availableFigureTypes);
    }
}