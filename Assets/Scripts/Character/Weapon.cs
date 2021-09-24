using System;
using Core;
using UniRx;
using UnityEngine;

namespace Character
{
    public class Weapon : ExtendedMonoBehaviour
    {
        public float baseAttack = 0.0f;
        public SpriteRenderer weaponSprite;
        public float visualizeTime = 0.2f;
        public Transform projectileSpawner;


        private IDisposable _update;
        private float _attackTime = -0.2f;

        public void Start()
        {
            _update = Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    weaponSprite.enabled = Time.time - _attackTime < visualizeTime;
                });
        }

        public void Attack()
        {
            _attackTime = Time.time;
        }

    }
}