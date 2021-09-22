using System;
using Core;
using Helpers;
using UniRx;
using UnityEngine;
using Zenject;

public class DestroyOnRicochetCount : GameComponent
{
    [SerializeField] private float _maxRicochetCount;

    [Inject] private RegistrationMap _registrationMap;
    
    private DataHolder _dataHolder;
    private IDisposable _ricochetCountSub;

    private void Start()
    {
        _dataHolder = this.GetOrAddComponent<DataHolder>();
        _ricochetCountSub = _dataHolder.Properties
            .GetOrCreate(Attributes.RicochetCount)
            .AsObservable()
            .Subscribe(CheckOnRicochetCount);
    }

    private void OnDestroy()
    {
        _ricochetCountSub?.Dispose();
    }

    private void CheckOnRicochetCount(float x)
    {
        if (x > _maxRicochetCount)
        {
            _registrationMap.UnregisterObject(gameObject);
            Destroy(gameObject);
        }
    }
}
