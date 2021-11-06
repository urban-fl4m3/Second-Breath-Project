using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SecondBreath.Game.Characters.Configs
{
    [CreateAssetMenu(fileName = "BattleCharactersConfig", menuName = "Configs/BattleCharactersConfig")]
    public class BattleCharactersConfig : SerializedScriptableObject
    {
        [SerializeField] private BattleCharacterData[] _charactersData;

        public IEnumerable<BattleCharacterData> CharactersData => _charactersData;
    }
}