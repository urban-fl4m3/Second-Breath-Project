using System;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Health
{
    public class HealthComponent : ActorComponent, IHealth
    {
        public event EventHandler<float> HealthRemained;
        
        public float MaximumHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        
        private IStatDataContainer _statDataContainer;
        
        public void Init(IDebugLogger logger, IStatDataContainer statDataContainer)
        {
            base.Init(logger);

            _statDataContainer = statDataContainer;

            MaximumHealth = _statDataContainer.GetStatValue(Stat.MaximumHealth);
            CurrentHealth = MaximumHealth;
        }

        public void AddHealth(float health)
        {
            var diff = CurrentHealth + health;
            CurrentHealth = Mathf.Min(diff, MaximumHealth);
            
            HealthRemained?.Invoke(this, CurrentHealth);
        }

        public void RemoveHealth(float health)
        {
            var diff = CurrentHealth - health;
            CurrentHealth = Mathf.Max(0, diff);
            
            HealthRemained?.Invoke(this, CurrentHealth);
        }
    }
}