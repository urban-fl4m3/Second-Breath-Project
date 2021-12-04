using System.Collections.Generic;
using Common.Actors;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public class Ability
    {
        private readonly IActor _caster;
        private readonly int _level;
        private readonly DiContainer _container;

        private readonly Dictionary<IMechanic, IMechanicData> _mechanics = new Dictionary<IMechanic, IMechanicData>();
        
        public Ability(IActor caster, AbilityData data, int level, DiContainer container)
        {
            _caster = caster;
            _level = level;
            _container = container;
            
            foreach (var mechanicData in data.Mechanics)
            {
                var mechanic = (IMechanic)container.Instantiate(mechanicData.LogicInstanceType);
                _mechanics.Add(mechanic, mechanicData);
            }
        }

        public void Enable()
        {
            foreach (var mechanic in _mechanics)
            {
                mechanic.Key.Init(_caster, mechanic.Value, _level, _container);
            }
        }

        public void Disable()
        {
            foreach (var mechanic in _mechanics)
            {
                mechanic.Key.Dispose();
            }
        }
    }
}