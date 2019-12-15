using CommandLine;
using System.ComponentModel.DataAnnotations;

namespace ReignTools.Entities.Options
{
    [Verb("unworthyroll", HelpText = "Roll a poll of a number of dices for unworthy opponents taken in parameter")]
    public class UnworthyRollOptions
    {
        [Option('n', "numberOfDice", HelpText = "Number of dice in the pool. Must be between 2 and 15"), Required]
        public short NumberOfDice { get; set; }

        public bool IsValid() => NumberOfDice >= 2 && NumberOfDice <= 15;
    }
}
