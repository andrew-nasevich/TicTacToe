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


        public GameConfigurationService(IPlayerRegistrationService playerRegistrationService, IConsole console, IGameConfigurationFactory gameConfigurationFactory)
        {
            _playerRegistrationService = playerRegistrationService;
            _console = console;
            _gameConfigurationFactory = gameConfigurationFactory;
        }


        public IGameConfiguration GetGameConfiguration()
        {
            var boardSize = GetBoardSize();
            var players = GetPlayers();
            var firstStepPlayer = GetFirstStepPlayer(players);

            return _gameConfigurationFactory.CreateGameConfiguration(boardSize, firstStepPlayer, players);
        }


        private IPlayer GetFirstStepPlayer(IReadOnlyCollection<IPlayer> players)
        {
            _console.WriteLine("Players:");
            players.ForEach(1, (i, p) => _console.WriteLine($"{i}) {p}"));

            int chosenPlayerIndex;
            do
            {
                _console.WriteLine("Please, white the number of the player who makes first step:");
                chosenPlayerIndex = _console.ReadInt();
            } while (chosenPlayerIndex <= 0 || chosenPlayerIndex > players.Count);

            return players.ElementAt(chosenPlayerIndex - 1);
        }

        private IReadOnlyCollection<IPlayer> GetPlayers()
        {
            var availableFigures = Enum.GetValues(typeof(FigureType)).Cast<FigureType>().ToList();
            var amountOfPlayers = GetAmountOfPlayers(availableFigures.Count);
            var players = new List<IPlayer>();

            for(var i = 0; i < amountOfPlayers; i++)
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
                _console.WriteLine("Please, write amount of players: ");
                amountOfPlayers = _console.ReadInt();
            } while (amountOfPlayers <= 1 || amountOfPlayers > amountOfFigureTypes);

            return amountOfPlayers;
        }

        private int GetBoardSize()
        {
            int boardSize;
            do
            {
                _console.WriteLine("Please, write board size: ");
                boardSize = _console.ReadInt();
            } while (boardSize <= 1);

            return boardSize;
        }
    }
}