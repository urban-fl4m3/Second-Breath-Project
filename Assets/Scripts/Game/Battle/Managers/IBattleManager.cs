using System;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Managers
{
    public interface IBattleManager
    {
        event EventHandler UnitsPrepared;
        
        IPlayer Player { get; }
        void PrepareUnits();
    }
}