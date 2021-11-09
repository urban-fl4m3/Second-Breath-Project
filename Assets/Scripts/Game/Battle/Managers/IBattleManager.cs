using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Managers
{
    public interface IBattleManager
    {
        IPlayer Player { get; }
        
        IBattlePreparationController GetPreparationController();
    }
}