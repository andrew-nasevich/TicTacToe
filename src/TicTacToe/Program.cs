using System;
using TicTacToe.Console.Boards;
using TicTacToe.Console.Interfaces;
using TicTacToe.Console.Console;
using TicTacToe.Console.Figures;
using TicTacToe.Console.GameConfigurations;
using TicTacToe.Console.Players;
using TicTacToe.Foundation.Boards;
using TicTacToe.Foundation.Cells;
using TicTacToe.Foundation.Figures;
using TicTacToe.Foundation.GameConfigurations;
using TicTacToe.Foundation.Games;
using TicTacToe.Foundation.Games.GameResults;
using TicTacToe.Foundation.Games.StepResults;
using TicTacToe.Foundation.Interfaces;
using TicTacToe.Foundation.Players;
using TicTacToe.Foundation.WinningStates;

namespace TicTacToe
{
    public class Program
    {
        private static IConsole _console;
        private static IBoardDrawer _boardDrawer;

        public static void Main(string[] args)
        {
            _console = new GameConsole();

            var figureDrawerFactory = new FigureDrawerFactory(_console);
            var figureDrawerProvider = new FigureDrawerProvider(figureDrawerFactory); 
            _boardDrawer = new BoardDrawer(figureDrawerProvider, _console);

            var gameInputProvider = new GameInputProvider(_console);
            var playerFactory = new PlayerFactory();

            var playerRegistrationService = new PlayerRegistrationService(playerFactory, _console);
            var gameConfigurationFactory = new GameConfigurationFactory();
            var gameConfigurationService = new GameConfigurationService(playerRegistrationService, _console, gameConfigurationFactory);

            var cellFactory = new CellFactory();
            var figureFactory = new FigureFactory();
            var boardFactory = new BoardFactory(cellFactory, figureFactory);

            var winningStateFactory = new WinningStateFactory();

            IGameConfiguration gameConfiguration = null;
            int chosenOption;
            do
            {
                gameConfiguration = gameConfigurationService.GetGameConfiguration(gameConfiguration);

                var game = new Game(gameInputProvider, gameConfiguration, boardFactory, winningStateFactory);
                game.GameStepCompleted += OnGameStepCompleted;
                game.GameFinished += OnGameFinished;

                game.Run();
                
                _console.WriteLine("Next possible options:");
                _console.WriteLine("1) Start new game with new players");
                _console.WriteLine("2) Start new game with the same players");
                _console.WriteLine("3) Exit");

                do
                {
                    chosenOption = _console.ReadInt("Please, choose an option: ");
                } while (chosenOption <= 0 || chosenOption > 3);

                if (chosenOption == 1)
                {
                    gameConfiguration = null;
                }
                
            } while (chosenOption != 3);
        }


        private static void OnGameFinished(object sender, GameFinishedEventArgs e)
        {
            var gameResult = e.GameResult;
            switch (gameResult.GameResultType)
            {
                case GameResultType.Draw:
                    _console.WriteLine("Friendship won!");
                    break;
                case GameResultType.Win:
                    var winResult = (WinGameResult)gameResult;
                    _console.WriteLine($"Player {winResult.WonPlayer.Name} won!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown GameResultType value: {gameResult.GameResultType}");
            }
        }

        private static void OnGameStepCompleted(object sender, GameStepCompletedEventArgs e)
        {
            var stepResult = e.StepResult;
            switch (stepResult.StepResultType)
            {
                case StepResultType.Success:
                    var successResult = (SuccessStepResult)stepResult;
                    _boardDrawer.DrawBoard(successResult.Board);
                    break;
                case StepResultType.CellIsAlreadyFilled:
                    var filledCellResult = (CellIsAlreadyFilledStepResult)stepResult;
                    _console.WriteLine($"This cell is already filled with figure: {filledCellResult.Cell.Figure.Type}");
                    break;
                case StepResultType.InvalidCellPosition:
                    var invalidResult = (InvalidCellPositionStepResult)stepResult;
                    _console.WriteLine($"This is no cell at position: ({invalidResult.SelectedColumn},{invalidResult.SelectedRow})");
                    break;
                default:
                    throw  new ArgumentOutOfRangeException($"Unknown StepResultType value: {stepResult.StepResultType}");
            }
        }
    }
}