using SecondBreath.Common.Logger;
using SecondBreath.Common.States;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.States.Concrete
{
    public class BattleState : BaseState
    {
        private readonly IGameTickHandler _tickHandler;
        private readonly ITickUpdate _debugTick;
        
        public BattleState(IGameTickHandler tickHandler, IDebugLogger logger)
        {
            _tickHandler = tickHandler;
            _debugTick = new TestTick(logger);
        }

        protected override void OnEnter()
        {
            _tickHandler.StartTicking();
            _tickHandler.AddTick(_debugTick);
        }

        protected override void OnExit()
        {
            _tickHandler.RemoveTick(_debugTick);
            _tickHandler.StopTicking();
        }
        
        private class TestTick : ITickUpdate 
        {
            private readonly IDebugLogger _logger;

            public TestTick(IDebugLogger logger)
            {
                _logger = logger;
            }

            public void Update()
            {
                _logger.Log("Battle scene tick");
            }
        }
    }
}