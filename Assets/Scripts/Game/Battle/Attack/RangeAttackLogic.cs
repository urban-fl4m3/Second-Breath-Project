using System;
using Common.Animations;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using UnityEngine;

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
            _target.DealDamage(damageData);
        }
    }
}