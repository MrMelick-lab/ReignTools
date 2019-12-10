using ReignTools.Entities.Options;
using System;
using System.Collections.Generic;

namespace ReignTools.Service
{
    public class DiceRollerService : IDiceRollerService
    {
        private readonly IDiceResultsInterpreterService diceResultsInterpreterService;
        private readonly IDiceResultUIService diceResultUIService;

        public DiceRollerService(IDiceResultsInterpreterService diceResultsInterpreterService, IDiceResultUIService diceResultUIService)
        {
            this.diceResultsInterpreterService = diceResultsInterpreterService;
            this.diceResultUIService = diceResultUIService;
        }

        public int Roll(RollOptions rollOptions)
        {
            if (!rollOptions.IsValid())
            {
                return -1;
            }

            var diceResults = RollPoolOfDice(rollOptions.NumberOfDice);

            var results = diceResultsInterpreterService.GetSetsFromDiceRolls(diceResults);

            diceResultUIService.ShowResults(results);

            return 0;
        }

        private static List<short> RollPoolOfDice(short numberOfDice)
        {
            var random = new Random();
            var diceResults = new List<short>();

            for (int i = 0; i < numberOfDice; i++)
            {
                diceResults.Add((short)random.Next(2, 10));
            }

            return diceResults;
        }
    }
}
