using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace SB.Managers
{
    public abstract class InjectManager : IDisposable
    {
        [Inject] protected DiContainer _container;

        public IReactiveProperty<bool> IsInit => _isInit;
        protected readonly ReactiveProperty<bool> _isInit = new ReactiveProperty<bool>();

        public void Activate()
        {
            if (_isInit.Value)
            {
                Debug.LogError($"Manager {GetType()} is already activated");
                return;
            }
            
            _isInit.Value = true;
        }
        
        public void Dispose()
        {
            _isInit.Value = false;
            _isInit?.Dispose();
            OnDispose();
        }

        protected virtual void OnActivate()
        {
            
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}