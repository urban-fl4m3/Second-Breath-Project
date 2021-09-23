using System;
using Core;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class DestroyOnCollide : GameComponent
{
    [Inject] private RegistrationMap _registrationMap;
    
    private IDisposable _destroyAction;
    
    private void Start()
    {
        _destroyAction = this
            .OnCollisionEnter2DAsObservable()
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            });
    }

    private void OnDestroy()
    {
        _destroyAction?.Dispose();
    }
}
