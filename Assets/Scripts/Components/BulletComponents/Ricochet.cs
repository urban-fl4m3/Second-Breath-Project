using Core;
using Helpers;
using UnityEngine;

namespace Components.BulletComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ricochet : GameComponent
    {
        private Rigidbody2D _rigidbody2D;
        private DataHolder _dataHolder;
        private Vector3 _prevVelocity;

        public override void Activate()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _dataHolder = this.GetOrAddComponent<DataHolder>();
            float x = 0.0f;
            _dataHolder.Properties.AddProperty(Attributes.RicochetCount, x);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _prevVelocity = _rigidbody2D.velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var newVelocity = Vector3.Reflect(_prevVelocity, other.GetContact(0).normal);
            _rigidbody2D.velocity = newVelocity;
            _dataHolder.Properties.GetOrCreateProperty<float>(Attributes.RicochetCount).Value += 1.0f;
        }
    }
}