using System;
using Helpers;
using UniRx;
using UnityEngine;

public class DestroyOnRicochetCount : MonoBehaviour
{
    [SerializeField] float _maxRicochetCount;
    
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
        _ricochetCountSub.Dispose();
    }

    private void CheckOnRicochetCount(float x)
    {
        if (x > _maxRicochetCount)
        {
            Destroy(gameObject);
        }
    }
}
