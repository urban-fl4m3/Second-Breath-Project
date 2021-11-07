using SecondBreath.Common.Logger;
using SecondBreath.Common.States;
using SecondBreath.Game.States.Concrete;

namespace SecondBreath.Game.States
{
    public class GameStateMachine : BaseStateMachine<GameState>, IGameStateMachine
    {
        private readonly IStateFactory _stateFactory;

        public GameStateMachine(IStateFactory stateFactory, IDebugLogger logger) : base(logger)
        {
            _stateFactory = stateFactory;
        }

        public void SetState(GameState state)
        {
            ChangeState(state);
        }

        public void CreateInitialStates()
        {
            _states.Add(GameState.Menu, _stateFactory.GetState<MenuState>());
            _states.Add(GameState.Board, _stateFactory.GetState<BoardState>());
            _states.Add(GameState.Battle, _stateFactory.GetState<BattleState>());
        }
    }
}