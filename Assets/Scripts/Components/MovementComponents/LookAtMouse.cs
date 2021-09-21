using System;
using UniRx;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private float _rotationFactor;

    private IDisposable _rotationApply;
    
    private void Start()
    {
        _rotationApply = Observable
            .EveryUpdate()
            .Subscribe(x => ApplyRotation());
    }

    private void OnDestroy()
    {
        _rotationApply?.Dispose();
    }

    private void ApplyRotation()
    {
        var forwardVec = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        var newRotation = Quaternion.LookRotation(Vector3.forward, forwardVec);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationFactor);
    }
}
