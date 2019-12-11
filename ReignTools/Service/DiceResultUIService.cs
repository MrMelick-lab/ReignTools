using ConsoleTableExt;
using ReignTools.Entities.Business;
using System.Collections.Generic;
using System.Linq;

namespace ReignTools.Service
{
    public class DiceResultUIService : IDiceResultUIService
    {
        public void ShowResults(List<Sets> resultsToShow)
        {
            if (!resultsToShow.Any())
            {
                return;
            }
            ConsoleTableBuilder
                .From(resultsToShow)
                .WithFormat(ConsoleTableBuilderFormat.MarkDown)
                .ExportAndWriteLine();
        }

        public void ShowResults(List<short> resultsToShow)
        {
            if (!resultsToShow.Any())
            {
                return;
            }
            ConsoleTableBuilder
                .From(resultsToShow)
                .WithFormat(ConsoleTableBuilderFormat.MarkDown)
                .ExportAndWriteLine();
        }

    }
}
