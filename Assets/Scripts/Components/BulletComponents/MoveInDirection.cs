using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public void Init()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddImpulse(Vector3 movementDirection, float speed)
    {
        _rigidbody2D.AddForce(movementDirection.normalized * speed, ForceMode2D.Impulse);
    }
}
