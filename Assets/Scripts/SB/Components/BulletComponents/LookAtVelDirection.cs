using System;
using SB.Core;
using UniRx;
using UnityEngine;

namespace SB.Components
{
    public class LookAtVelDirection : GameComponent
    {
        private Rigidbody2D _rigidbody2D;
        private IDisposable _updater;
        
        public override void Activate()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _updater = Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var newRotation = Quaternion.LookRotation(Vector3.forward, _rigidbody2D.velocity.normalized * -1.0f);
                    transform.rotation = newRotation;
                });
        }

        private void OnDestroy()
        {
            _updater?.Dispose();
        }
    }
}