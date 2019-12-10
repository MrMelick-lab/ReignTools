using ReignTools.Entities.Business;
using System.Collections.Generic;

namespace ReignTools.Service
{
    public interface IDiceResultUIService
    {
        void ShowResults(List<Sets> resultsToShow);
    }
}