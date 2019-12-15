using ReignTools.Entities.Options;

namespace ReignTools.Service
{
    public interface IDiceRollerService
    {
        int Roll(RollOptions rollOptions);
        int Roll(UnworthyRollOptions rollOptions);
    }
}