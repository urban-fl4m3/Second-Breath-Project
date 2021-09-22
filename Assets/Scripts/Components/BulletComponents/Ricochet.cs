using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private DataHolder _dataHolder;

    private Vector3 _prevVelocity;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _dataHolder = Utilities.GetOrAddComponent<DataHolder>(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _prevVelocity = _rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var newVelocity = _prevVelocity;
        newVelocity = Vector3.Reflect(_prevVelocity, other.GetContact(0).normal);
        _rigidbody2D.velocity = newVelocity;
        _dataHolder.properties[DataEnum.attributes.RicochetCount].Value += 1.0f;
    }
}
