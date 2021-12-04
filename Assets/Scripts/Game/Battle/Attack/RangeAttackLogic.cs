using System;
using SecondBreath.Game.Battle.Attack.Projectiles;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Stats;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SecondBreath.Game.Battle.Attack
{
    public class RangeAttackLogic : BaseAttackLogic
    {
        protected override void HandleAttackEvent(object sender, EventArgs e)
        {
            _isAttacking = false;
            _animationEventHandler.Unsubscribe(_attackEvent);
        
            _lastAttackTime = Time.time;

            var damageData = new DamageData(_statDataContainer.GetStatValue(Stat.AttackDamage));
            
            var newProjectile = Object.Instantiate(_data.Projectile, _projectileSpawner.position, Quaternion.identity).GetComponent<TargetedProjectile>();
            newProjectile.Init(_target, damageData);
        }
    }
}