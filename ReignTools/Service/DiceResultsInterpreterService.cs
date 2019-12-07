using ReignTools.Entities.Business;
using System.Collections.Generic;
using System.Linq;

namespace ReignTools.Service
{
    public class DiceResultsInterpreterService : IDiceResultsInterpreterService
    {
        public List<Sets> GetSetsFromDiceRolls(List<short> dicesResults)
        {
            return dicesResults.GroupBy(x => x)
                               .Where(g => g.Count() > 1)
                               .Select(y => new Sets{ Height = y.Key, Width = (short)y.Count() })
                               .OrderByDescending(w => w.Width)
                               .ToList();
        }
    }
}
