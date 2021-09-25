using SB.Core;
using UnityEngine;

namespace SB.Components.BulletComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveInDirection : GameComponent
    {
        private Rigidbody2D _rigidbody2D;

        public override void Activate()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void AddImpulse(Vector3 movementDirection, float speed)
        {
            _rigidbody2D.AddForce(movementDirection.normalized * speed, ForceMode2D.Impulse);
        }
    }
}