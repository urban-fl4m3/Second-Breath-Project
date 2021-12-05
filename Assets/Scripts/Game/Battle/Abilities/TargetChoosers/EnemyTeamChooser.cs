using System.Collections.Generic;
using System.Linq;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class EnemyTeamChooser : ITargetChooser
    {
        private readonly ITeamObjectRegisterer<IActor> _actorRegisterer;
        private IActor _owner;
        
        public EnemyTeamChooser(ITeamObjectRegisterer<IActor> actorRegisterer)
        {
            _actorRegisterer = actorRegisterer;
        }

        public List<IActor> ChooseTarget()
        {
            return _actorRegisterer.GetOppositeTeamObjects(_owner.Owner.Team).ToList();
        }

        public void Init(IActor actor)
        {
            _owner = actor;
        }
    }
}