using System;
using SB.Core;
using UniRx;
using UnityEngine;

namespace SB.Components.MovementComponents
{
    public class LookAtMouse : GameComponent
    {
        [SerializeField] private float _rotationFactor;

        private IDisposable _rotationApply;

        public override void Activate()
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
}