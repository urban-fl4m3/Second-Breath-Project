using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Characters.Configs
{
    [CreateAssetMenu(fileName = "BattleCharactersConfig", menuName = "Configs/BattleCharactersConfig")]
    public class BattleCharactersConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<int, BattleCharacterData> _charactersData;

        public IReadOnlyDictionary<int, BattleCharacterData> CharactersData => _charactersData;
    }
}