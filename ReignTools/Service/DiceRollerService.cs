using ConsoleTableExt;
using ReignTools.Entities.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReignTools.Service
{
    public class DiceRollerService : IDiceRollerService
    {
        private IDiceResultsInterpreterService diceResultsInterpreterService;

        public DiceRollerService(IDiceResultsInterpreterService diceResultsInterpreterService)
        {
            this.diceResultsInterpreterService = diceResultsInterpreterService;
        }

        public int Roll(RollOptions rollOptions)
        {
            if (!rollOptions.IsValid())
            {
                return -1;
            }

            var diceResults = new List<short>();
            var random = new Random();

            for (int i = 0; i < rollOptions.NumberOfDice; i++)
            {
                diceResults.Add((short)random.Next(2, 10));
            }

            var results = diceResultsInterpreterService.GetSetsFromDiceRolls(diceResults);

            ConsoleTableBuilder.From(results)
                                .WithFormat(ConsoleTableBuilderFormat.MarkDown)
                                .ExportAndWriteLine();
            return 0;
        }
    }
}
