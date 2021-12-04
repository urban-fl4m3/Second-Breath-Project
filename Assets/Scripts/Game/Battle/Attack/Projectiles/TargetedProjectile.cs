using Common.Actors;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Movement.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SecondBreath.Game.Battle.Attack.Projectiles
{
    public class TargetedProjectile : SerializedMonoBehaviour
    {
        private IActor _target;
        private Vector3 _startPos;
        private Vector3 _finishPos;
        private DamageData _damageData;
        [SerializeField]private float _projectileSpeed;

        private float _lerpDelta;
        private float _lerpCoef;
        
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
            _lerpCoef = 0.0f;
        }
        private void Update()
        {
            _lerpCoef += _lerpDelta * Time.deltaTime;
            _lerpCoef = Mathf.Clamp(_lerpCoef, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(_startPos, _finishPos, _lerpCoef);

            if (_lerpCoef >= 1.0f)
            {
                _target.Components.Get<IDamageable>().DealDamage(_damageData);
                Destroy(gameObject);
            }
        }
    }
}