using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Common.EnumerableExtensions;
using TicTacToe.Console.Interfaces;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.Interfaces;

namespace TicTacToe.Console.GameConfigurations
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private readonly IPlayerRegistrationService _playerRegistrationService;
        private readonly IConsole _console;
        private readonly IGameConfigurationFactory _gameConfigurationFactory;


        public GameConfigurationService(
            IPlayerRegistrationService playerRegistrationService, 
            IConsole console,
            IGameConfigurationFactory gameConfigurationFactory)
        {
            _playerRegistrationService = playerRegistrationService;
            _console = console;
            _gameConfigurationFactory = gameConfigurationFactory;
        }


        public IGameConfiguration GetGameConfiguration(IGameConfiguration existingGameConfiguration = null)
        {
            var boardSize = GetBoardSize();
            var players = existingGameConfiguration != null ? existingGameConfiguration.Players : GetPlayers();
            var firstStepPlayer = GetFirstStepPlayer(players);

            return _gameConfigurationFactory.CreateGameConfiguration(boardSize, players, firstStepPlayer);
        }


        private int GetBoardSize()
        {
            int boardSize;
            do
            {
                boardSize = _console.ReadInt("Please, write board size: ");
            } while (boardSize <= 1);

            return boardSize;
        }

        private IReadOnlyCollection<IPlayer> GetPlayers()
        {
            var availableFigures = Enum.GetValues(typeof(FigureType)).Cast<FigureType>().ToList();
            var amountOfPlayers = GetAmountOfPlayers(availableFigures.Count);
            var players = new List<IPlayer>();

            for (var i = 0; i < amountOfPlayers; i++)
            {
                var player = _playerRegistrationService.RegisterPlayer(availableFigures);
                players.Add(player);
                availableFigures.Remove(player.FigureType);
                _console.WriteLine($"Player {player.Name} was registered.");
            }

            return players;
        }

        private int GetAmountOfPlayers(int amountOfFigureTypes)
        {
            int amountOfPlayers;
            do
            {
                amountOfPlayers = _console.ReadInt("Please, write amount of players: ");
            } while (amountOfPlayers <= 1 || amountOfPlayers > amountOfFigureTypes);

            return amountOfPlayers;
        }

        private IPlayer GetFirstStepPlayer(IReadOnlyCollection<IPlayer> players)
        {
            _console.WriteLine("Players:");
            players.ForEach((p, i) => _console.WriteLine($"{i + 1}) {p.Name}"));

            int chosenNumberOfPlayer;
            do
            {
                chosenNumberOfPlayer = _console.ReadInt("Please, write the number of the player who makes first step:");
            } while (chosenNumberOfPlayer <= 0 || chosenNumberOfPlayer > players.Count);

            return players.ElementAt(chosenNumberOfPlayer - 1);
        }
    }
}