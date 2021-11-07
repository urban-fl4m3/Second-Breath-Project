using SecondBreath.Common.States;

namespace SecondBreath.Game.States
{
    public interface IGameStateMachine : IStateMachine<GameState>
    {
        void CreateInitialStates();
        void SetState(GameState state);
    }
}