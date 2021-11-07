using System;
using UniRx;

namespace SecondBreath.Common.States
{
    public interface IStateMachine<TStateType> where TStateType : Enum
    {
        IReadOnlyReactiveProperty<TStateType> CurrentState { get; }
    }
}