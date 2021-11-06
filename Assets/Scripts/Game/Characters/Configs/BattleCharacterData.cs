using System.Collections.Generic;
using SecondBreath.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Characters.Configs
{
    [CreateAssetMenu(fileName = "BattleCharacterData", menuName = "Configs/Characters/BattleCharacterData")]
    public class BattleCharacterData : SerializedScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [OdinSerialize] private Dictionary<Stat, StatData> _stats;

        public GameObject Prefab => _prefab;
        public IReadOnlyDictionary<Stat, StatData> Stats => _stats;
    }
}