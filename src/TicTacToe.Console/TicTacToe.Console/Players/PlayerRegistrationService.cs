using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Common.EnumerableExtensions;
using TicTacToe.Foundation.Interfaces;
using TicTacToe.Foundation.Figures;
using TicTacToe.Console.Interfaces;

namespace TicTacToe.Console.Players
{
    public class PlayerRegistrationService : IPlayerRegistrationService
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IConsole _console;


        public PlayerRegistrationService(
            IPlayerFactory playerFactory,
            IConsole console)
        { 
            _playerFactory = playerFactory;
            _console = console;
        }


        public IPlayer RegisterPlayer(IReadOnlyCollection<FigureType> availableFigureTypes)
        {
            var name = _console.ReadString("Please, write player's name: ");
            var figureType = ChooseFigure(availableFigureTypes);

            return _playerFactory.CreatePlayer(name, figureType);
        }


        private FigureType ChooseFigure(IReadOnlyCollection<FigureType> availableFigureTypes)
        {
            if (!availableFigureTypes.Any())
            {
                throw new InvalidOperationException("There is no more available figure types");
            }
            if (availableFigureTypes.Count == 1)
            {
                _console.WriteLine($"There is only one figure type left. You figure type: {availableFigureTypes.First().ToString()}");

                return availableFigureTypes.Single();
            }

            _console.WriteLine("Available figures types:");
            availableFigureTypes.ForEach((ft, i) => _console.WriteLine($"{i + 1}) {ft}"));

            int chosenFigureTypeNum;
            do
            {
                chosenFigureTypeNum = _console.ReadInt("Please, write your figure type number:");
            } while (chosenFigureTypeNum <= 0 || chosenFigureTypeNum > availableFigureTypes.Count);

            return availableFigureTypes.ElementAt(chosenFigureTypeNum - 1);
        }
    }
}