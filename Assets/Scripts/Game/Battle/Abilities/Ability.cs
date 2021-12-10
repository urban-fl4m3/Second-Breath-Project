using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Abilities.Triggers;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public class Ability
    {
        private readonly IActor _caster;
        private readonly int _level;
        private readonly DiContainer _container;

        private readonly List<IMechanic> _mechanics = new List<IMechanic>();
        private readonly List<ITrigger> _triggers = new List<ITrigger>();
        
        public Ability(IActor caster, AbilityData data, int level, DiContainer container)
        {
            _caster = caster;
            _level = level;
            _container = container;
            
            foreach (var mechanicData in data.Mechanics)
            {
                var mechanic = (IMechanic)container.Instantiate(mechanicData.LogicInstanceType);
                mechanic.Init(_caster, mechanicData, _level, _container);
                _mechanics.Add(mechanic);
            }

            foreach (var trigger in data.Triggers)
            {
                var newTrigger = (ITrigger) container.Instantiate(trigger.GetType());
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