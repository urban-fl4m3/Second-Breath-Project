﻿using System.Linq;
using SecondBreath.Common.Extensions;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters.Actors;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats.Formulas;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Characters
{
    public class BattleCharactersFactory
    {
        private readonly IDebugLogger _logger;
        private readonly DiContainer _diContainer;
        private readonly IStatUpgradeFormula _statUpgradeFormula;
        private readonly BattleCharactersConfig _battleCharactersConfig;

        public BattleCharactersFactory(DiContainer diContainer, BattleCharactersConfig battleCharactersConfig, 
            IDebugLogger logger, IStatUpgradeFormula formula)
        {
            _logger = logger;
            _diContainer = diContainer;
            _battleCharactersConfig = battleCharactersConfig;
            _statUpgradeFormula = formula;
        }

        public void SpawnRandomCharacter(IPlayer owner, Vector3 initialPosition)
        {
            var charactersData = _battleCharactersConfig.CharactersData;
            var keys = charactersData.Keys.ToArray();
            
            var randomIndex = Random.Range(0, keys.Length);
            var randomKey = keys[randomIndex];

            SpawnCharacter(randomKey, owner, initialPosition);
        }

        public void SpawnCharacter(int id, IPlayer owner, Vector3 initialPosition)
        {
            var randomCharacterData = _battleCharactersConfig.CharactersData.GetValue(id);
            var prefab = randomCharacterData.Prefab;

            var characterInstance = _diContainer.InstantiatePrefab(prefab);
            var battleCharacter = characterInstance.GetComponent<BattleCharacter>();
            
            battleCharacter.Init(owner, randomCharacterData, _statUpgradeFormula, _logger, initialPosition);
        }
    }
}