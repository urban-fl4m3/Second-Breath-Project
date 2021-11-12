using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Ticks;
using Zenject;

namespace SecondBreath.Game.Battle.Movement.Components
{
    public class MovementComponent : ActorComponent
    {
        [Inject] private IGameTickCollection _tickHandler;
        
        private IStatDataContainer _statContainer;
        private MovementUpdate _movement;
        
        public void Init(IDebugLogger logger, IStatDataContainer statContainer, IReadOnlyComponentContainer components)
        {
            base.Init(logger);

            _statContainer = statContainer;
            
            //get components
            //get rotation component
            //get target searcher component? 
            
            _movement = new MovementUpdate(transform, _statContainer.GetStatValue(Stat.MovementSpeed));
            
            Enable();
        }

        public override void Enable()
        {
            base.Enable();  
            
            _tickHandler.AddTick(_movement);
        }

        public override void Disable()
        {
            base.Disable();
            
            _tickHandler.RemoveTick(_movement);
        }
    }
}