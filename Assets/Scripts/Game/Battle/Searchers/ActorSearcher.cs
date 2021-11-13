using System;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;
using Zenject;
using UniRx;

namespace SecondBreath.Game.Battle.Searchers
{
    public class ActorSearcher : BaseTargetSearcher<IActor>
    {
        [Inject] private ITeamObjectRegisterer<IActor> _actorsRegisterer;
        [Inject] private IGameTickCollection _tickHandler;

        private ActorSearcherUpdate _actorSearcherUpdate;
        private ITranslatable _translatable;
        private IDisposable _targetSearchSub;

        public void Init(IDebugLogger logger, Team ownerTeam, IReadOnlyComponentContainer container)
        {
            base.Init(logger, ownerTeam);

            _translatable = container.Get<ITranslatable>();
        }

        public override void Enable()
        {
            base.Enable();
            
            _actorSearcherUpdate = new ActorSearcherUpdate(_actorsRegisterer, OwnerTeam, Target, _translatable);
            
            _targetSearchSub = Target.Subscribe(OnTargetChanged);
        }

        private void OnTargetChanged(IActor target)
        {
            if (target == null)
            {
                _tickHandler.AddTick(_actorSearcherUpdate);
            }
            else
            {
                _tickHandler.RemoveTick(_actorSearcherUpdate);
            }
        }

        public override void Disable()
        {
            base.Disable();
            
            _actorSearcherUpdate?.Dispose();
            _targetSearchSub?.Dispose();
        }
    }
}