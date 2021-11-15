using System;

namespace SecondBreath.Game.Battle.Health
{
    public interface IReadOnlyHealth
    {
        event EventHandler<float> HealthRemained;
        
        float MaximumHealth { get; }
        float CurrentHealth { get; }
    }
}