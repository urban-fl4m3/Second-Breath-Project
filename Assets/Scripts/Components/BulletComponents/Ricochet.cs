using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 hitPos;
    private Vector3 hitNormal;
    private Vector3 reflectPos;
    private Vector3 reflectNormal;
    private Vector3 velocityOnHit;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.cyan;
        // Gizmos.DrawRay(hitPos, hitNormal * 5);
        // Gizmos.color = Color.red;
        // Gizmos.DrawRay(reflectPos, reflectNormal * 5);
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawRay(reflectPos, velocityOnHit * 5);
        // if (_rigidbody2D)
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawRay(transform.position, _rigidbody2D.velocity.normalized * 5);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var currentVelocity = _rigidbody2D.velocity;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentVelocity);
        print(hit.normal + " ");
        velocityOnHit = transform.up;
        hitPos = hit.point;
        hitNormal = hit.normal;
        currentVelocity = Vector2.Reflect(currentVelocity, hitNormal);
        reflectPos = hitPos;
        reflectNormal = currentVelocity;
        _rigidbody2D.velocity = currentVelocity;
    }
}
