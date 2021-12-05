using System;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class UnregisterTrigger : ITrigger
    {
        private ITeamObjectRegisterer<IActor> _teamObjectRegisterer;

        public UnregisterTrigger(ITeamObjectRegisterer<IActor> teamObjectRegisterer)
        {
            _teamObjectRegisterer = teamObjectRegisterer;
        }
        public void Dispose()
        {
            _teamObjectRegisterer.ObjectUnregistered -= Action;
        }

        private void Action(object obj, RegistrationTeamObjectArgs actorArgs)
        {
            Events?.Invoke(obj, actorArgs);
        }

        public event EventHandler<EventArgs> Events;
        public void Init(IActor actor)
        {
            _teamObjectRegisterer.ObjectUnregistered += Action;
        }
    }
}