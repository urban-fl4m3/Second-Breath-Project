using System;
using System.Collections.Generic;
using SecondBreath.Common.Logger;
using UniRx;

namespace SecondBreath.Common.States
{
    public abstract class BaseStateMachine<TStateType> : IStateMachine<TStateType> where TStateType : Enum
    {
        public IReadOnlyReactiveProperty<TStateType> CurrentState => _currentState;

        protected readonly IDebugLogger _logger;
        protected readonly Dictionary<TStateType, IState> _states = new Dictionary<TStateType, IState>();

        private readonly ReactiveProperty<TStateType> _currentState = new ReactiveProperty<TStateType>();
        
        protected BaseStateMachine(IDebugLogger logger)
        {
            _logger = logger;
        }

        protected void ChangeState(TStateType stateType)
        {
            if (stateType.Equals(_currentState.Value))
            {
                _logger.LogWarning($"You are trying to change similar state ({stateType})");
                return;
            }

            if (_currentState.Value != null)
            {
                _states[_currentState.Value].Exit();
            }

            _currentState.Value = stateType;
            
            _states[_currentState.Value].Enter();
        }
    }
}