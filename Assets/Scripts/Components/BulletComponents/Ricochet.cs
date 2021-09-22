using Helpers;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ricochet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private DataHolder _dataHolder;
    private Vector3 _prevVelocity;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _dataHolder = this.GetOrAddComponent<DataHolder>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _prevVelocity = _rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var newVelocity = Vector3.Reflect(_prevVelocity, other.GetContact(0).normal);
        _rigidbody2D.velocity = newVelocity;
        _dataHolder.Properties[Attributes.RicochetCount].Value += 1.0f;
    }
}
