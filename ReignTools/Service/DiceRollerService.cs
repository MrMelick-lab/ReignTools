using ReignTools.Entities.Options;
using System;
using System.Collections.Generic;
using System.Linq;

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

            List<short> diceResults = new List<short>();
            bool specialDice = rollOptions.ExpertDice > 0 || rollOptions.MasterDice;

            if (rollOptions.NumberOfDice == 10 && specialDice)
            {
                rollOptions.NumberOfDice -= 1;
            }

            if (rollOptions.ExpertDice > 0)
            {
                diceResults.Add(rollOptions.ExpertDice);

            }

            diceResults.AddRange(RollPoolOfDice(rollOptions.NumberOfDice));

            if (rollOptions.MasterDice)
            {
                diceResultUIService.ShowResults(diceResults);
                var masterDice = Convert.ToInt16(Console.ReadLine());
                if(masterDice < 2 || masterDice > 10)
                {
                    return -1;
                }
                diceResults.Add(masterDice);
            }

            var interpretedDiceResult = diceResultsInterpreterService.GetSetsFromDiceRolls(diceResults);

            diceResultUIService.ShowResults(interpretedDiceResult);

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
