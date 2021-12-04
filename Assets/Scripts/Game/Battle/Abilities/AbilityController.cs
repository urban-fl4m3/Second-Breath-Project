using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Common.Logger;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public class AbilityController : ActorComponent
    {
        [Inject] private DiContainer _container;
        
        private readonly List<Ability> _abilities = new List<Ability>();
        
        public void Init(IActor owner, int level, IDebugLogger logger, IEnumerable<AbilityData> abilitiesData)
        {
            if (abilitiesData == null)
            {
                return;
            }
            
            base.Init(logger);

            foreach (var abilityData in abilitiesData)
            {
                var ability = new Ability(owner, abilityData, level, _container);
                _abilities.Add(ability);
            }
        }

        public override void Enable()
        {
            foreach (var ability in _abilities)
            {
                ability.Enable();
            }
        }

        public override void Disable()
        {
            foreach (var ability in _abilities)
            {
                ability.Disable();
            }
        }
    }
}