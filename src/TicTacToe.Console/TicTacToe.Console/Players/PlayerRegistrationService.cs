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
            _console.WriteLine("Please, enter player's name: ");
            var name = _console.ReadString();
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
            availableFigureTypes.ForEach(1, (i, ft) => _console.WriteLine($"{i}) {ft}"));

            int chosenFigureTypeIndex;
            do
            {
                _console.WriteLine("Please, write your figure type number:");
                chosenFigureTypeIndex = _console.ReadInt();
            } while (chosenFigureTypeIndex <= 0 || chosenFigureTypeIndex > availableFigureTypes.Count);

            return availableFigureTypes.ElementAt(chosenFigureTypeIndex - 1);
        }
    }
}