using ConsoleTableExt;
using ReignTools.Entities.Business;
using System.Collections.Generic;

namespace ReignTools.Service
{
    public class DiceResultUIService : IDiceResultUIService
    {
        public void ShowResults(List<Sets> resultsToShow)
        {
            ConsoleTableBuilder
                .From(resultsToShow)
                .WithFormat(ConsoleTableBuilderFormat.MarkDown)
                .ExportAndWriteLine();
        }
    }
}
