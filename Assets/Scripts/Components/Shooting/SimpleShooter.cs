using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SimpleShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletInitialPosition;
    public float attackSpeed;
    public float bulletInitialSpeed;
    private float _timeToNextAttack = 0.0f;
    private IDisposable _shooter;
    void Start()
    {
        _shooter = Observable
            .EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.Mouse0))
            .Subscribe(x =>
            {
                Shoot();
            });
    }
    
    private void OnDestroy()
    {
        _shooter?.Dispose();
    }

    void Shoot()
    {
        _timeToNextAttack -= Time.deltaTime;
        if (_timeToNextAttack > 0)
        {
            return;
        }

        _timeToNextAttack += attackSpeed;


        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletInitialPosition.transform.position;
        var moveInDirection = newBullet.AddComponent<MoveInDirection>();
        moveInDirection.Init();
        moveInDirection.AddImpulse(transform.up, bulletInitialSpeed);
    }
}
