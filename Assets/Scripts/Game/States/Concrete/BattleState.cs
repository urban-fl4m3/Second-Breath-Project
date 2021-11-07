using SecondBreath.Common.Logger;
using SecondBreath.Common.States;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle;
using SecondBreath.Game.Battle.Ticks;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.States.Concrete
{
    public class BattleState : BaseState
    {
        private readonly IGameTickHandler _tickHandler;
        private readonly IBattleScene _battleScene;
        private readonly IDebugLogger _logger;

        private ITickUpdate _mouseClickOnField;
        
        public BattleState(IGameTickHandler tickHandler, IBattleScene battleScene, IDebugLogger logger)
        {
            _tickHandler = tickHandler;
            _battleScene = battleScene;
            _logger = logger;
        }

        protected override void OnEnter()
        {
            _mouseClickOnField = new BattleFieldPointSelection(_battleScene.Field, _logger);
            _tickHandler.StartTicking();
            _tickHandler.AddTick(_mouseClickOnField);
        }

        protected override void OnExit()
        {
            _tickHandler.RemoveTick(_mouseClickOnField);
            _tickHandler.StopTicking();
        }
    }
}