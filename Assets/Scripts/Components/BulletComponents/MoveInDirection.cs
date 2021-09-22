using Core;
using UnityEngine;

namespace Components.BulletComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveInDirection : GameComponent
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void AddImpulse(Vector3 movementDirection, float speed)
        {
            _rigidbody2D.AddForce(movementDirection.normalized * speed, ForceMode2D.Impulse);
        }
    }
}