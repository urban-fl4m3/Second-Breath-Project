using System;
using SB.Core;
using SB.Helpers;
using UnityEngine;

namespace SB.Components
{
    public class DestroyOnRicochetCount : GameComponent
    {
        [SerializeField] private float _maxRicochetCount;

        private IDisposable _ricochetCountSub;

        public override void Activate()
        {
            _dataHolder = this.GetOrAddComponent<DataHolder>();
            _ricochetCountSub = _dataHolder.Properties
                .GetOrCreateProperty<float>(Attributes.RicochetCount)
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
                Destroy(gameObject);
            }
        }
    }
}