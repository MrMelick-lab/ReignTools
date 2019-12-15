#nullable enable
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
                               .ThenByDescending(h => h.Height)
                               .ToList();
        }

        public List<Sets> GetSetsFromUnworthyDiceRolls(List<short> dicesResults)
        {
            var unfilteredResults = dicesResults.GroupBy(x => x)
                                    .Where(g => g.Count() > 1)
                                    .Select(y => new Sets { Height = y.Key, Width = (short)y.Count() })
                                    .OrderByDescending(w => w.Width)
                                    .ThenByDescending(h => h.Height)
                                    .ToList();

            var resultsWithAWidthBiggerThanTwo = unfilteredResults.FindAll(w => w.Width > 2);
            if (resultsWithAWidthBiggerThanTwo.Any())
            {
                return SplitBigSetsInSetsOfWidthOfTwo(unfilteredResults, resultsWithAWidthBiggerThanTwo);
            }

            return unfilteredResults;
        }

        private static List<Sets> SplitBigSetsInSetsOfWidthOfTwo(List<Sets> unfilteredResults, List<Sets> resultsWithAWidthBiggerThanTwo)
        {
            var filteredResults = unfilteredResults.Except(resultsWithAWidthBiggerThanTwo).ToList();
            foreach (var results in resultsWithAWidthBiggerThanTwo)
            {
                var numberOfPair = results.Width / 2;
                for (var i = 0; i < numberOfPair; i++)
                {
                    filteredResults.Add(new Sets
                    {
                        Height = results.Height,
                        Width = 2
                    });
                }
            }
            return filteredResults.OrderByDescending(h => h.Height)
                                  .ToList();
        }
    }
}
