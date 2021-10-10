using System;
using SB.Core;
using UniRx;
using UniRx.Triggers;

namespace SB.Components
{
    public class DestroyOnCollide : GameComponent
    {
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