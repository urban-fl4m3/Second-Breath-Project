using System;
using System.Collections;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Abilities.Triggers;
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
        private float _hpAmount;
        private IHealable _healable;
        private IReadOnlyHealth _readOnlyHealth;
        private BattleCharacterAnimator _animator;
        private bool isRevived;
        
        
        protected override void OnInit(ReviveData data)
        {
            _hpAmount = StatUpgradeFormula.GetValue(Data.HPAmount, Level);
            _healable = Caster.Components.Get<IHealable>();
            _animator = Caster.Components.Get<BattleCharacterAnimator>();
        }

        private IEnumerator DelayForAction()
        {
            yield return new WaitForSeconds(Data.Delay);
            _actorRegisterer.Register(Caster.Owner.Team, Caster);
            Caster.Enable();
            
            var newHP = _healable.Health.MaximumHealth * (_hpAmount / 100.0f);
            _healable.Heal(new HealData(newHP));
            _animator.SetReviveTrigger();
        }

        public override void Action(object sender, EventArgs args)
        {
            if (isRevived) return;
            Observable.FromCoroutine(DelayForAction).Subscribe();
            isRevived = true;
        }
    }
}