using ReignTools.Entities.Options;
using System.Collections.Generic;

namespace ReignTools.Service
{
    public interface IDiceRollerService
    {
        int Roll(RollOptions rollOptions);
        int Roll(UnworthyRollOptions rollOptions);
        List<short> RollPoolOfDice(short numberOfDice);
    }
}