using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Ticks;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Movement.Components
{
    public class MovementComponent : ActorComponent, ITranslatable
    {
        [Inject] private IGameTickCollection _tickHandler;
    
        public Vector3 Position => _transform.position;
        
        private IStatDataContainer _statContainer;
        private MovementUpdate _movement;
        private Transform _transform;

        private ActorSearcher _actorSearcher;
        
        public void Init(IDebugLogger logger, IStatDataContainer statContainer, IReadOnlyComponentContainer components)
        {
            base.Init(logger);

            _transform = transform;
            _statContainer = statContainer;
            
            //get components
            //get rotation component
            _actorSearcher = components.Get<ActorSearcher>();
        }

        public override void Enable()
        {
            base.Enable();  
            
            _movement = new MovementUpdate(_actorSearcher, _transform, _statContainer.GetStatValue(Stat.MovementSpeed));
            _tickHandler.AddTick(_movement);
        }

        public override void Disable()
        {
            base.Disable();
            
            _tickHandler.RemoveTick(_movement);
        }
    }
}