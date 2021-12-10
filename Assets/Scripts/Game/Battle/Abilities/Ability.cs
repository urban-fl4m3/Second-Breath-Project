using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Abilities.Triggers;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public class Ability
    {
        private readonly List<IMechanic> _mechanics = new List<IMechanic>();
        private readonly List<ITrigger> _triggers = new List<ITrigger>();
        
        public Ability(IActor caster, AbilityData data, int level, DiContainer container)
        {
            foreach (var mechanicData in data.Mechanics)
            {
                var mechanic = (BaseMechanic)container.Instantiate(mechanicData.LogicInstanceType);
                mechanic.Init(caster, mechanicData, level, container);
                _mechanics.Add(mechanic);
            }

            foreach (var trigger in data.Triggers)
            {
                var newTrigger = (BaseAbilityTrigger) container.Instantiate(trigger.GetType());
                newTrigger.Init(caster);
                _triggers.Add(newTrigger);
            }
        }

        public void Enable()
        {
            foreach (var mechanic in _mechanics)
            {
                mechanic.Register(_triggers);
            }
        }

        public void Disable()
        {
            foreach (var mechanic in _mechanics)
            {
                mechanic.UnRegister(_triggers);
            }
        }
    }
}