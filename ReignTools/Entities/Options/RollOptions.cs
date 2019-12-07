using CommandLine;
using System.ComponentModel.DataAnnotations;

namespace ReignTools.Entities.Options
{
    [Verb("roll", HelpText = "Roll a poll of a number of dices taken in parameter")]
    public class RollOptions
    {
         [Option('n', "numberOfDice", HelpText = "Number of dice in the pool. Must be between 2 and 10"), Required]
         public short NumberOfDice { get; set; }

        public bool IsValid() => NumberOfDice >= 2 && NumberOfDice <= 10;
    }
}
