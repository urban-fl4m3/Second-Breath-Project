using System.Collections.Generic;
using SecondBreath.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Characters.Configs
{
    [CreateAssetMenu(fileName = "BattleCharacterData", menuName = "Configs/Characters/BattleCharacterData")]
    public class BattleCharacterData : SerializedScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [OdinSerialize] private Dictionary<Stat, StatData> _stats;
        [SerializeField] [Min(0.0f)] private float _radius;
        [SerializeField] [Min(0.0f)] private float _height;
        
        public GameObject Prefab => _prefab;
        public IReadOnlyDictionary<Stat, StatData> Stats => _stats;
        public float Radius => _radius;
        public float Height => _height;
    }
}