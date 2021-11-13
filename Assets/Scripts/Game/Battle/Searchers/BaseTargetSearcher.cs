using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Players;
using UniRx;

namespace SecondBreath.Game.Battle.Searchers
{
    public abstract class BaseTargetSearcher<T> : ActorComponent
    {
        public IReadOnlyReactiveProperty<T> CurrentTarget => Target;

        protected Team OwnerTeam { get; private set; }
        protected IReactiveProperty<T> Target { get; private set; }
        
        protected void Init(IDebugLogger logger, Team ownerTeam)
        {
            base.Init(logger);

            Target = new ReactiveProperty<T>();
            OwnerTeam = ownerTeam;
        }
    }
}