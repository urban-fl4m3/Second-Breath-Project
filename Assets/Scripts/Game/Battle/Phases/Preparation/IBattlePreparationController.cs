using System;

namespace SecondBreath.Game.Battle.Phases
{
    public interface IBattlePreparationController
    {
        event EventHandler UnitsPrepared;

        void PrepareUnits();
    }
}