using SB.Components.BulletComponents;
using SB.Components.Data;
using SB.Core;
using SB.Helpers;
using UniRx;
using UnityEngine;
using Zenject;

namespace SB.Skills.Logic
{
    public class ShootLogic : BaseSkillLogic
    {
        [Inject] private RegistrationMap _registrationMap;
        
        private GameObject _projectile;
        private float _damage;
        private float _count;
        private Transform bulletInitialPos;

        private readonly CompositeDisposable _shootingActions = new CompositeDisposable();

        private float _timeToNextAttack;

        private GameObject _owner;
        private Battle.Character _character;

        public override void SetData(DataModel dataModel)
        {
            _projectile = dataModel.GetOrCreateProperty<GameObject>(Attributes.ProjectilePrefab).Value;
            _damage = dataModel.GetOrCreateProperty<float>(Attributes.Damage).Value;
            _count = dataModel.GetOrCreateProperty<float>(Attributes.ProjectileCount).Value;
        }

        public override void Activate(GameObject owner)
        {
            _owner = owner;
            _character = _owner.GetComponent<Battle.Character>();
            
            _shootingActions.Add(Observable
                .EveryUpdate()
                .Where(_ => Input.GetKey(KeyCode.Mouse0))
                .Subscribe(_ => { Shoot(); }));

            _shootingActions.Add(Observable
                .EveryUpdate()
                .Subscribe(_ => { ReduceTimeToNextAttack(); }));
        }

        public override void Deactivate()
        {
            _shootingActions?.Dispose();
        }
        
        private void ReduceTimeToNextAttack()
        {
            _timeToNextAttack -= Time.deltaTime;
        }

        private void Shoot()
        {
            var weapon = _character._weapon;
            
            if (weapon == null) return;
            
            weapon.Value.Attack();
            
            if (_timeToNextAttack > 0)
            {
                return;
            }

            _timeToNextAttack = 1.0f;


            CreateBullet(weapon.Value.projectileSpawner.position, weapon.Value.transform.rotation);
        }

        private void CreateBullet(Vector3 initialPosition, Quaternion initialRotation)
        {
            var newBullet = Object.Instantiate(_projectile, initialPosition,
                initialRotation);
            newBullet.ActivateGameComponents();
            newBullet.GetComponent<MoveInDirection>().AddImpulse(_owner.transform.up, 10);
        }
    }
}