using System;
using SB.Core;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace SB.Components.BulletComponents
{
    public class DestroyOnCollide : GameComponent
    {
        [Inject] private RegistrationMap _registrationMap;

        private IDisposable _destroyAction;

        public override void Activate()
        {
            _destroyAction = this
                .OnCollisionEnter2DAsObservable()
                .Subscribe(_ => { Destroy(gameObject); });
        }

        private void OnDestroy()
        {
            _destroyAction?.Dispose();
        }
    }
}