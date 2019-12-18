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
        private static readonly IConsole Console;
        private static readonly IBoardDrawer BoardDrawer;


        static Program()
        {
            Console = new GameConsole();

            var figureDrawerFactory = new FigureDrawerFactory(Console);
            var figureDrawerProvider = new FigureDrawerProvider(figureDrawerFactory);
            BoardDrawer = new BoardDrawer(figureDrawerProvider, Console);
        }


        public static void Main(string[] args)
        {
            var playerFactory = new PlayerFactory();
            var playerRegistrationService = new PlayerRegistrationService(playerFactory, Console);
            var gameConfigurationFactory = new GameConfigurationFactory();
            var gameConfigurationService = new GameConfigurationService(playerRegistrationService, Console, gameConfigurationFactory);

            var gameInputProvider = new GameInputProvider(Console);
            var cellFactory = new CellFactory();
            var figureFactory = new FigureFactory();
            var boardFactory = new BoardFactory(cellFactory, figureFactory);
            var winningStateFactory = new WinningStateFactory();
            var gameFactory = new GameFactory(gameInputProvider, boardFactory, winningStateFactory);

            IGameConfiguration gameConfiguration = null;
            int chosenOption;
            do
            {
                gameConfiguration = gameConfigurationService.GetGameConfiguration(gameConfiguration);
                var game = gameFactory.CreateGame(gameConfiguration);

                game.GameStepCompleted += OnGameStepCompleted;
                game.GameFinished += OnGameFinished;

                game.Run();

                game.GameStepCompleted -= OnGameStepCompleted;
                game.GameFinished -= OnGameFinished;

                Console.WriteLine("Next possible options:");
                Console.WriteLine("1) Start new game with new players");
                Console.WriteLine("2) Start new game with the same players");
                Console.WriteLine("3) Exit");

                do
                {
                    chosenOption = Console.ReadInt("Please, choose an option: ");
                } while (chosenOption < 0 || chosenOption >= 3);

                if (chosenOption == 1)
                {
                    gameConfiguration = null;
                }
                
            } while (chosenOption != 3);
        }


        private static void OnGameStepCompleted(object sender, GameStepCompletedEventArgs e)
        {
            var stepResult = e.StepResult;
            switch (stepResult.StepResultType)
            {
                case StepResultType.Success:
                    var successResult = (SuccessStepResult)stepResult;
                    BoardDrawer.DrawBoard(successResult.Board);
                    break;
                case StepResultType.CellIsAlreadyFilled:
                    var filledCellResult = (CellIsAlreadyFilledStepResult)stepResult;
                    Console.WriteLine($"This cell is already filled with figure: {filledCellResult.Cell.Figure.Type}");
                    break;
                case StepResultType.InvalidCellPosition:
                    var invalidResult = (InvalidCellPositionStepResult)stepResult;
                    Console.WriteLine($"This is no cell at position: ({invalidResult.SelectedColumn},{invalidResult.SelectedRow})");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown StepResultType value: {stepResult.StepResultType}");
            }
        }

        private static void OnGameFinished(object sender, GameFinishedEventArgs e)
        {
            var gameResult = e.GameResult;
            switch (gameResult.GameResultType)
            {
                case GameResultType.Draw:
                    Console.WriteLine("Friendship won!");
                    break;
                case GameResultType.Win:
                    var winResult = (WinGameResult)gameResult;
                    Console.WriteLine($"Player {winResult.WonPlayer.Name} won!");
                    Console.Write("Winning combination: ");
                    foreach (var cell in winResult.WinningCollection)
                    {
                        Console.Write($"({cell.Row + 1},{cell.Column + 1}) ");
                    }
                    Console.WriteLine();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown GameResultType value: {gameResult.GameResultType}");
            }
        }
    }
}