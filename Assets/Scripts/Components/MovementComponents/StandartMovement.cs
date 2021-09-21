using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float movementSpeed;

    private Vector2 moveVector;
    private bool isPressed;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        isPressed = false;
        moveVector = new Vector2(0.0f, 0.0f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isPressed = true;
            moveVector = moveVector + new Vector2(0.0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            isPressed = true;
            moveVector = moveVector + new Vector2(0.0f, -1.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            isPressed = true;
            moveVector = moveVector + new Vector2(-1.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            isPressed = true;
            moveVector = moveVector + new Vector2(1.0f, 0.0f);
        }
        moveVector.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody2D.AddForce(moveVector * movementSpeed / 2.0f, ForceMode2D.Impulse);
        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, movementSpeed);
        _rigidbody2D.drag = isPressed ? 0.0f : movementSpeed / 2.0f;
        
        isPressed = false;
        moveVector = new Vector2(0.0f, 0.0f);
    }
}
