using ReignTools.Entities.Business;
using System.Collections.Generic;

namespace ReignTools.Service
{
    public interface IDiceResultsInterpreterService
    {
        List<Sets> GetSetsFromDiceRolls(List<short> dicesResults);
        List<Sets> GetSetsFromUnworthyDiceRolls(List<short> dicesResults);
    }
}