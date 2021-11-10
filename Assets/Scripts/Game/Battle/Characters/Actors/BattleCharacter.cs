using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats;

namespace SecondBreath.Game.Battle.Characters.Actors
{
    public class BattleCharacter : Actor
    {
        private IPlayer _owner;
        
        public void Init(IPlayer owner, IReadOnlyDictionary<Stat, StatData> stats)
        {
            _owner = owner;
        }
    }
}