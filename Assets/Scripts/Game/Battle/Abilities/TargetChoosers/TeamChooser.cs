using System;
using System.Collections.Generic;
using System.Linq;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class TeamChooser : ITargetChooser
    {
        private readonly ITeamObjectRegisterer<IActor> _actorRegisterer;
        private IActor _owner;
        [OdinSerialize] private Side side;

        public TeamChooser(ITeamObjectRegisterer<IActor> actorRegisterer)
        {
            _actorRegisterer = actorRegisterer;
        }

        public List<IActor> ChooseTarget()
        {
            return side == Side.Ally ? _actorRegisterer.GetTeamObjects(_owner.Owner.Team).ToList() : _actorRegisterer.GetOppositeTeamObjects(_owner.Owner.Team).ToList();
        }

        public void Init(IActor actor, ITargetChooserData data)
        {
            _owner = actor;
            side = ((TeamChooserData) data).ChoosenSide;
        }
    }
}