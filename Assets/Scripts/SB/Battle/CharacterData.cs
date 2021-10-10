using System.Collections.Generic;
using SB.Common.Attributes;
using Sirenix.Serialization;
using UnityEngine;

namespace SB.Battle
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Configs/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private GameObject _character;
        [OdinSerialize] private Dictionary<AttributeType, float> _attributes;

        public GameObject Character => _character;
        public IReadOnlyDictionary<AttributeType, float> Attributes => _attributes;
    }
}