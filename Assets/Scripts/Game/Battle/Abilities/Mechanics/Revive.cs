using System;
using System.Collections;
using Common.Actors;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Health;
using SecondBreath.Game.Battle.Registration;
using UniRx;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class Revive : BaseMechanic<ReviveData>
    {
        [Inject] private ITeamObjectRegisterer<IActor> _actorRegisterer;
        
        private IHealable _healable;
        private BattleCharacterAnimator _animator;
        
        private bool isRevived;
        private float _hpAmount;

        private IDisposable _reviveSub;
        
        protected override void OnInit()
        {
            _hpAmount = StatUpgradeFormula.GetValue(Data.HPAmount, Level);
            _healable = Caster.Components.Get<IHealable>();
            _animator = Caster.Components.Get<BattleCharacterAnimator>();
        }

        private IEnumerator ReviveDelay()
        {
            yield return new WaitForSeconds(Data.Delay);
            
            _reviveSub?.Dispose();
            _reviveSub = null;
            
            _actorRegisterer.Register(Caster.Owner.Team, Caster);
            Caster.Enable();
            
            var newHP = _healable.Health.MaximumHealth * (_hpAmount / 100.0f);
            var healthData = new HealData(newHP);
            
            _healable.Heal(healthData);
            _animator.SetReviveTrigger();
        }

        protected override void ApplyMechanic(object sender, EventArgs args)
        {
            if (isRevived) return;
            
            _reviveSub = Observable.FromCoroutine(ReviveDelay).Subscribe();
            isRevived = true;
        }
    }
}