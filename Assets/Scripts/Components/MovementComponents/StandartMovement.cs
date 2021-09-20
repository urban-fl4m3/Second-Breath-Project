using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D; 
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isPressed = false;
        if (Input.GetKey(KeyCode.W))
        {
            isPressed = true;
            _rigidbody2D.AddForce(new Vector2(0.0f, 5.0f), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            isPressed = true;
            _rigidbody2D.AddForce(new Vector2(0.0f, -5.0f), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            isPressed = true;
            _rigidbody2D.AddForce(new Vector2(-5.0f, 0.0f), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            isPressed = true;
            _rigidbody2D.AddForce(new Vector2(5.0f, 0.0f), ForceMode2D.Impulse);
        }

        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, 10.0f);

        if (!isPressed)
        {
            _rigidbody2D.drag = 5.0f;
        }
        else
        {
            _rigidbody2D.drag = 0.0f;
        }
    }
}
