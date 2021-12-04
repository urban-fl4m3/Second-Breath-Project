using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(AbilityData), menuName = "Configs/Abilities/Ability Data")]
    public class AbilityData : SerializedScriptableObject
    {
        [OdinSerialize] private IMechanicData[] _mechanics;

        public IEnumerable<IMechanicData> Mechanics => _mechanics;
    }
}