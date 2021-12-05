using System;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class UnregisterTrigger : ITrigger
    {
        private ITeamObjectRegisterer<IActor> _teamObjectRegisterer;
        private IActor _owner;
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
            if (actorArgs.Obj == _owner) return;
            Events?.Invoke(obj, actorArgs);
        }

        public event EventHandler<EventArgs> Events;
        public void Init(IActor actor)
        {
            _owner = actor;
            _teamObjectRegisterer.ObjectUnregistered += Action;
        }
    }
}