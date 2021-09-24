using Components.BulletComponents;
using Components.Data;
using Core;
using UniRx;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Skills.Logic
{
    public class ShootLogic : BaseSkillLogic
    {
        [Inject] private RegistrationMap _registrationMap;
        
        private GameObject _projectile;
        private float _damage;
        private float _count;

        private readonly CompositeDisposable _shootingActions = new CompositeDisposable();

        private float _timeToNextAttack;

        private GameObject _owner;

        public override void SetData(DataModel dataModel)
        {
            _projectile = dataModel.GetOrCreateProperty<GameObject>(Attributes.ProjectilePrefab).Value;
            _damage = dataModel.GetOrCreateProperty<float>(Attributes.Damage).Value;
            _count = dataModel.GetOrCreateProperty<float>(Attributes.ProjectileCount).Value;
        }

        public override void Activate(GameObject owner)
        {
            _owner = owner;
            
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
            if (_timeToNextAttack > 0)
            {
                return;
            }

            _timeToNextAttack = 1.0f;


            CreateBullet();
        }

        private void CreateBullet()
        {
            var newBullet = Object.Instantiate(_projectile, _owner.transform.position + _owner.transform.up,
                Quaternion.identity);
            
            newBullet.GetComponent<MoveInDirection>().AddImpulse(_owner.transform.up, 10);
        }
    }
}