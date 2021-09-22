using System;
using System.Collections.Generic;
using Core;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InputMovement : GameComponent
{
    [SerializeField] private float _movementFactor;
    
    private Vector2 _moveVector = Vector2.zero;
    private Rigidbody2D _rigidbody2D;
    private bool _isPressed;

    private List<IDisposable> _inputs;
    private IDisposable _velocityApply;

    private void Start()
    {
        _inputs = new List<IDisposable>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _isPressed = false;

        _inputs.Add(Observable
            .EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.W))
            .Subscribe(x =>
            {
                _isPressed = true;
                _moveVector += new Vector2(0.0f, 1.0f);
                _moveVector.Normalize();
            }));
        
        _inputs.Add(Observable
            .EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.S))
            .Subscribe(x =>
            {
                _isPressed = true;
                _moveVector += new Vector2(0.0f, -1.0f);
                _moveVector.Normalize();
            }));
        
        _inputs.Add(Observable
            .EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.A))
            .Subscribe(x =>
            {
                _isPressed = true;
                _moveVector += new Vector2(-1.0f, 0.0f);
                _moveVector.Normalize();
            }));
        
        _inputs.Add(Observable
            .EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.D))
            .Subscribe(x =>
            {
                _isPressed = true;
                _moveVector += new Vector2(1.0f, 0.0f);
                _moveVector.Normalize();
            }));

        _velocityApply = Observable
            .EveryFixedUpdate()
            .Subscribe(x =>
            {
                ApplyVelocity();
            });
    }

    private void ApplyVelocity()
    {
        _rigidbody2D.AddForce(_moveVector * _movementFactor / 2.0f, ForceMode2D.Impulse);
        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, _movementFactor);
        _rigidbody2D.drag = _isPressed ? 0.0f : _movementFactor / 2.0f;
        
        _isPressed = false;
        _moveVector = new Vector2(0.0f, 0.0f);
    }

    private void OnDestroy()
    {
        foreach (var input in _inputs)
        {
            input?.Dispose();
        }
        
        _velocityApply?.Dispose();
    }
}
