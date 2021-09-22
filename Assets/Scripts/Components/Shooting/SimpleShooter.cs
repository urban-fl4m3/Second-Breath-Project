using Components.BulletComponents;
using Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Components.Shooting
{
    public class SimpleShooter : GameComponent
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletInitialPosition;

        [Inject] private RegistrationMap _registrationMap;

        public float attackSpeed;
        public float bulletInitialSpeed;

        private float _timeToNextAttack;
        private readonly CompositeDisposable _shootingActions = new CompositeDisposable();

        private void Start()
        {
            _shootingActions.Add(Observable
                .EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.Mouse0))
                .Subscribe(_ => { Shoot(); }));

            _shootingActions.Add(Observable
                .EveryUpdate()
                .Subscribe(_ => { ReduceTimeToNextAttack(); }));
        }

        private void ReduceTimeToNextAttack()
        {
            _timeToNextAttack -= Time.deltaTime;
        }

        private void Shoot()
        {
            if (_timeToNextAttack > 0)
            {
                return;
            }

            _timeToNextAttack = attackSpeed;


            CreateBullet();
        }

        private void CreateBullet()
        {
            var newBullet = InstantiatePrefab(_bulletPrefab, _bulletInitialPosition.transform.position,
                Quaternion.identity);
            _registrationMap.RegisterObject(newBullet);
            newBullet.GetComponent<MoveInDirection>().AddImpulse(transform.up, bulletInitialSpeed);
        }

        private void OnDestroy()
        {
            _shootingActions?.Dispose();
        }
    }
}