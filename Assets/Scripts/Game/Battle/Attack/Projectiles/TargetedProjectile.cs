using Common.Actors;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SecondBreath.Game.Battle.Attack.Projectiles
{
    public class TargetedProjectile : SerializedMonoBehaviour
    {
        [SerializeField] private float _projectileSpeed;
        
        private IActor _target;
        private Vector3 _startPos;
        private Vector3 _finishPos;
        private DamageData _damageData;

        private float _lerpDelta;
        private float _lerpFactor;
        
        public void Init(IActor target, DamageData damageData)
        {
            _target = target;
            _damageData = damageData;
            _startPos = transform.position;

            var translatableComponent = _target.Components.Get<ITranslatable>();
            _finishPos = translatableComponent.Position.Value;
            _finishPos.y += translatableComponent.Height / 2.0f;
            
            transform.rotation = Quaternion.LookRotation(_finishPos - _startPos, Vector3.up);
            
            _lerpDelta = _projectileSpeed / Vector3.Distance(_startPos, _finishPos);
            _lerpFactor = 0.0f;
        }
        
        private void Update()
        {
            _lerpFactor += _lerpDelta * Time.deltaTime;
            _lerpFactor = Mathf.Clamp(_lerpFactor, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(_startPos, _finishPos, _lerpFactor);

            if (_lerpFactor >= 1.0f)
            {
                _target.Components.Get<IDamageable>().DealDamage(_damageData);
                Destroy(gameObject);
            }
        }
    }
}