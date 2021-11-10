using System;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Players;
using SecondBreath.Game.States.Concrete;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SecondBreath.Game.Battle.Phases
{
    public class EnemyPreparationController : IBattlePreparationController
    {
        public event EventHandler UnitsPrepared;

        private readonly IPlayer _player;
        private readonly IBattleField _battleField;
        private readonly BattleCharactersFactory _battleCharactersFactory;
        
        public EnemyPreparationController(IPlayer player, IBattleField battleField, BattleCharactersFactory battleCharactersFactory)
        {
            _player = player;
            _battleField = battleField;
            _battleCharactersFactory = battleCharactersFactory;
        }
        
        public void PrepareUnits()
        {
            for (var i = 0; i < BattleState.DEBUG_UNITS_COUNT; i++)
            {
                SpawnUnit();
            }
            
            UnitsPrepared?.Invoke(this, EventArgs.Empty);    
        }

        private void SpawnUnit()
        {
            var rect = _battleField.GetTeamRect(_player.Team);
            var rectPosition = rect.position;
            var xValue = Random.Range(rectPosition.x, rect.width + rectPosition.x);
            var yValue = Random.Range(rectPosition.y, rect.height + rectPosition.y);
            var spawnPosition = new Vector3(xValue, 0, yValue);
            
            _battleCharactersFactory.SpawnRandomCharacter(_player, spawnPosition);
        }
    }
}