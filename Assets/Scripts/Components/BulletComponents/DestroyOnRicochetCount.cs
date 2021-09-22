using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DestroyOnRicochetCount : MonoBehaviour
{
    public float maxRicochetCount;
    private DataHolder _dataHolder;
    private IDisposable _x;
    void Start()
    {
        _dataHolder = Utilities.GetOrAddComponent<DataHolder>(gameObject);
        Debug.Log(_dataHolder);
        _x = _dataHolder.properties.GetOrCreate(DataEnum.attributes.RicochetCount).AsObservable().Subscribe(x =>
        {
            CheckOnRicochetCount(x);
        });
    }
    
    private void OnDestroy()
    {
        _x.Dispose();
    }

    private void CheckOnRicochetCount(float x)
    {
        if (x > maxRicochetCount)
        {
            Destroy(gameObject);
        }
    }
}
