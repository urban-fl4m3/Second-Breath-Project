using System.Collections.Generic;
using System.Linq;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class AllyTeamChooser : ITargetChooser
    {
        private readonly ITeamObjectRegisterer<IActor> _actorRegisterer;
        private IActor _owner;
        
        public AllyTeamChooser(ITeamObjectRegisterer<IActor> actorRegisterer)
        {
            _actorRegisterer = actorRegisterer;
        }

        public List<IActor> ChooseTarget()
        {
            return _actorRegisterer.GetTeamObjects(_owner.Owner.Team).ToList();
        }

        public void Init(IActor actor)
        {
            _owner = actor;
        }
    }
}