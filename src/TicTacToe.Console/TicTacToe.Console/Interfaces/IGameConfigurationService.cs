using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.Interfaces
{
    public interface IGameConfigurationService
    {
        IGameConfiguration GetGameConfiguration(IGameConfiguration existingGameConfiguration);
    }
}