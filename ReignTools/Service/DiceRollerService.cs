using ReignTools.Entities.Options;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ReignTools.Service
{
    public class DiceRollerService : IDiceRollerService
    {
        private readonly IDiceResultsInterpreterService diceResultsInterpreterService;
        private readonly IDiceResultUIService diceResultUIService;
        private readonly IConsoleReaderService consoleReaderService;
        private static readonly Random getrandom = new Random();

        public DiceRollerService(IDiceResultsInterpreterService diceResultsInterpreterService, IDiceResultUIService diceResultUIService, IConsoleReaderService consoleReaderService)
        {
            this.diceResultsInterpreterService = diceResultsInterpreterService;
            this.diceResultUIService = diceResultUIService;
            this.consoleReaderService = consoleReaderService;
        }

        public int Roll(RollOptions rollOptions)
        {
            if (!rollOptions.IsValid())
            {
                return -1;
            }

            var diceResults = new List<short>();
            bool specialDice = rollOptions.ExpertDice > 0 || rollOptions.MasterDice;

            if (specialDice)
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
                var masterDice = consoleReaderService.ReadLine();
                if (masterDice == -1)
                {
                    return -1;
                }
                diceResults.Add(masterDice);
            }

            var interpretedDiceResult = diceResultsInterpreterService.GetSetsFromDiceRolls(diceResults);

            diceResultUIService.ShowResults(interpretedDiceResult);

            return 0;
        }

        public int Roll(UnworthyRollOptions rollOptions)
        {
            if (!rollOptions.IsValid())
            {
                return -1;
            }

            var diceResults = RollPoolOfDice(rollOptions.NumberOfDice);

            var interpretedDiceResult = diceResultsInterpreterService.GetSetsFromUnworthyDiceRolls(diceResults);

            diceResultUIService.ShowResults(interpretedDiceResult);

            return 0;
        }

        private static List<short> RollPoolOfDice(short numberOfDice)
        {
            var diceResults = new List<short>();

            for (int i = 0; i < numberOfDice; i++)
            {
                diceResults.Add(GetRandomNumber(2, 11));
                Thread.Sleep(50);
            }

            return diceResults;
        }

        private static short GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return (short)getrandom.Next(min, max);
            }
        }
    }
}
