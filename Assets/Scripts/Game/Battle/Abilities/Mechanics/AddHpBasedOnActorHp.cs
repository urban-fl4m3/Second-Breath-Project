using System;
using Common.Actors;
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
        private IHealable _healable;
        private ITranslatable _translatable;
        private Side _choosenSide;
        protected override void OnInit(AddHpBasedOnActorHpData data)
        {
            _healable = Caster.Components.Get<IHealable>();
            _translatable = Caster.Components.Get<ITranslatable>();
        
            _radius  = StatUpgradeFormula.GetValue(Data.Radius, Level);
            _hpPercent = StatUpgradeFormula.GetValue(Data.HpPercent, Level);
            _choosenSide = Data.ChoosenSide;
        }
        
        public override void Action(object sender, EventArgs args)
        {
            RegistrationTeamObjectArgs objectArgs = (RegistrationTeamObjectArgs) args;
            if (objectArgs == null) return;

            IActor diedActor = objectArgs.Obj;
            var diedActorComponents = diedActor.Components;
            
            Vector3 diedActorPosition = diedActorComponents.Get<ITranslatable>().Position.Value;
            Vector3 ourPosition = _translatable.Position.Value;

            if (Vector3.SqrMagnitude(diedActorPosition - ourPosition) > _radius * _radius) return;
            
            
            Team ourTeam = Caster.Owner.Team;
            Team diedActorTeam = diedActor.Owner.Team;

            if (ourTeam == diedActorTeam && _choosenSide != Side.Ally) return;
            if (ourTeam != diedActorTeam && _choosenSide != Side.Enemy) return;

            float healAmount = diedActorComponents.Get<IReadOnlyHealth>().MaximumHealth * (_hpPercent / 100.0f);
            HealData healData = new HealData(healAmount);
            _healable.Heal(healData);
        }
        
    }
}