using System;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Health;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class AddHpBasedOnActorHp : BaseMechanic<AddHpBasedOnActorHpData>
    {
        private float _radius;
        private float _hpPercent;
        
        private Side _chosenSide;
        private IHealable _healable;
        private ITranslatable _translatable;
        
        protected override void OnInit()
        {
            _healable = Caster.Components.Get<IHealable>();
            _translatable = Caster.Components.Get<ITranslatable>();
        
            _radius  = StatUpgradeFormula.GetValue(Data.Radius, Level);
            _hpPercent = StatUpgradeFormula.GetValue(Data.HpPercent, Level);
            _chosenSide = Data.ChoosenSide;
        }

        protected override void ApplyMechanic(object sender, EventArgs args)
        {
            var objectArgs = (RegistrationTeamObjectArgs) args;
            
            if (objectArgs == null) return;

            var diedActor = objectArgs.Obj;
            if (diedActor == Caster) return;
            
            var diedActorComponents = diedActor.Components;
            
            var diedActorPosition = diedActorComponents.Get<ITranslatable>().Position.Value;
            var ourPosition = _translatable.Position.Value;

            if (Vector3.SqrMagnitude(diedActorPosition - ourPosition) > _radius * _radius) return;
            
            var ourTeam = Caster.Owner.Team;
            var diedActorTeam = diedActor.Owner.Team;

            if (ourTeam == diedActorTeam && _chosenSide != Side.Ally) return;
            if (ourTeam != diedActorTeam && _chosenSide != Side.Enemy) return;

            var healAmount = diedActorComponents.Get<IReadOnlyHealth>().MaximumHealth * (_hpPercent / 100.0f);
            var healData = new HealData(healAmount);
            
            _healable.Heal(healData);
        }
        
    }
}