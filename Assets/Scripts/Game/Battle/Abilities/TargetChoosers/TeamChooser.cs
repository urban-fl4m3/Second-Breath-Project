using System.Collections.Generic;
using System.Linq;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class TeamChooser : BaseTargetChooser<TeamChooserData>
    {
        private Side _side;
        
        private readonly ITeamObjectRegisterer<IActor> _actorRegisterer;
        
        public TeamChooser(ITeamObjectRegisterer<IActor> actorRegisterer)
        {
            _actorRegisterer = actorRegisterer;
        }

        protected override void OnInit()
        {
            _side = Data.ChosenSide;
        }

        public override IEnumerable<IActor> ChooseTarget()
        {
            return _side == Side.Ally 
                ? _actorRegisterer.GetTeamObjects(Owner.Owner.Team).ToList()
                : _actorRegisterer.GetOppositeTeamObjects(Owner.Owner.Team).ToList();
        }
    }
}