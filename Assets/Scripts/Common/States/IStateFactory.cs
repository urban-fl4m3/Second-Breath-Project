namespace SecondBreath.Common.States
{
    public interface IStateFactory
    {
        IState GetState<TState>() where TState : IState;
    }
}