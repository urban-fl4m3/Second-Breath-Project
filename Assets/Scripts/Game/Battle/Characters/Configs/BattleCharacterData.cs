using System.Collections.Generic;
using SecondBreath.Game.Battle.Abilities;
using SecondBreath.Game.Battle.Attack;
using SecondBreath.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Characters.Configs
{
    [CreateAssetMenu(fileName = "BattleCharacterData", menuName = "Configs/Characters/BattleCharacterData")]
    public class BattleCharacterData : SerializedScriptableObject
    {
        [SerializeField] private string _unitName;
        [SerializeField] private Sprite _unitIcon;
        
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _projectile;
        [OdinSerialize] private Dictionary<Stat, StatData> _stats;
        [OdinSerialize] private BaseAttackLogic _attackLogic;
        [SerializeField] private AbilityData[] _abilitiesData;
        
        [SerializeField] [Min(0.0f)] private float _radius;
        [SerializeField] [Min(0.0f)] private float _height;

        public string Name => _unitName;
        public Sprite Icon => _unitIcon;

        public GameObject Prefab => _prefab;
        public GameObject Projectile => _projectile;
        public IReadOnlyDictionary<Stat, StatData> Stats => _stats;
        public BaseAttackLogic AttackLogic => _attackLogic;
        public IEnumerable<AbilityData> AbilitiesData => _abilitiesData;
        public float Radius => _radius;
        public float Height => _height;
    }
}