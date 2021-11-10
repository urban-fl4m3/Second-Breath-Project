﻿using System.Linq;
using Common.Actors;
using SecondBreath.Common.Extensions;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters.Actors;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Characters
{
    public class BattleCharactersFactory
    {
        private readonly DiContainer _diContainer;
        private readonly BattleCharactersConfig _battleCharactersConfig;
        private readonly ITeamObjectRegisterer<IActor> _actorRegisterer;

        public BattleCharactersFactory(DiContainer diContainer, BattleCharactersConfig battleCharactersConfig, 
            ITeamObjectRegisterer<IActor> actorRegisterer)
        {
            _diContainer = diContainer;
            _actorRegisterer = actorRegisterer;
            _battleCharactersConfig = battleCharactersConfig;
        }

        public void SpawnRandomCharacter(IPlayer owner, Vector3 initialPosition)
        {
            var charactersData = _battleCharactersConfig.CharactersData;
            var keys = charactersData.Keys.ToArray();
            
            var randomIndex = Random.Range(0, keys.Length);
            var randomKey = keys[randomIndex];

            var randomCharacterData = charactersData.GetValue(randomKey);
            var prefab = randomCharacterData.Prefab;

            var characterInstance = _diContainer.InstantiatePrefab(prefab, initialPosition, Quaternion.identity, null);
            var battleCharacter = characterInstance.GetComponent<BattleCharacter>();
            
            battleCharacter.Init(owner, randomCharacterData.Stats);
            
            _actorRegisterer.Register(owner.Team, battleCharacter); 
        }
    }
}