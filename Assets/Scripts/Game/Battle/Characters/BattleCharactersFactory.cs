using SecondBreath.Common.Extensions;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters.Configs;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Characters
{
    public class BattleCharactersFactory
    {
        public int CharactersCount => _battleCharactersConfig.CharactersData.Count;
        
        private readonly DiContainer _diContainer;
        private readonly BattleCharactersConfig _battleCharactersConfig;
        private readonly IDebugLogger _debugLogger;

        public BattleCharactersFactory(DiContainer diContainer, BattleCharactersConfig battleCharactersConfig,
            IDebugLogger debugLogger)
        {
            _diContainer = diContainer;
            _battleCharactersConfig = battleCharactersConfig;
            _debugLogger = debugLogger;
        }

        public GameObject CreateBattleCharacter(int id)
        {
            var prefab = _battleCharactersConfig.CharactersData.GetValue(id).Prefab;

            if (prefab == null)
            {
                _debugLogger.LogError($"Characters config doesn't have character with ID: {id}");
                return null;
            }
            
            return  _diContainer.InstantiatePrefab(prefab);
        }
    }
}