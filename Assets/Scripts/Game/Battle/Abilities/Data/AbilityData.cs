using System.Collections.Generic;
using SecondBreath.Game.Battle.Abilities.Triggers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(AbilityData), menuName = "Configs/Abilities/Ability Data")]
    public class AbilityData : SerializedScriptableObject
    {
        [OdinSerialize] private IMechanicData[] _mechanics;
        [OdinSerialize] private ITrigger[] _triggers;

        public IEnumerable<IMechanicData> Mechanics => _mechanics;
        public IEnumerable<ITrigger> Triggers => _triggers;
    }
}