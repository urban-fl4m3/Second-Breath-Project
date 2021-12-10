using Common.Actors;
using SecondBreath.Game.Battle.Registration;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class UnregisterTrigger : BaseAbilityTrigger
    {
        private readonly ITeamObjectRegisterer<IActor> _teamObjectRegisterer;
        
        public UnregisterTrigger(ITeamObjectRegisterer<IActor> teamObjectRegisterer)
        {
            _teamObjectRegisterer = teamObjectRegisterer;
        }

        public override void Dispose()
        {
            _teamObjectRegisterer.ObjectUnregistered -= Action;
        }

        protected override void OnInit()
        {
            _teamObjectRegisterer.ObjectUnregistered += Action;
        }
        
        private void Action(object obj, RegistrationTeamObjectArgs actorArgs)
        {
            Trigger(actorArgs);
        }
    }
}