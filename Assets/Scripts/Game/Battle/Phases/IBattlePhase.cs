using System;

namespace SecondBreath.Game.Battle.Phases
{
    public interface IBattlePhase
    {
        event EventHandler PhaseCompleted;
        
        void Run();
        void End();
    }
}