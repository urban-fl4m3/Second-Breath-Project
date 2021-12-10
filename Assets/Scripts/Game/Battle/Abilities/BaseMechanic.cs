using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;
using SecondBreath.Game.Battle.Abilities.Triggers;
using SecondBreath.Game.Stats.Formulas;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public abstract class BaseMechanic : IMechanic
    {
        protected IStatUpgradeFormula StatUpgradeFormula { get; private set; }
        protected IActor Caster { get; private set; }
        protected int Level { get; private set; }

        protected readonly List<ITargetChooser> _choosers = new List<ITargetChooser>();
        
        private DiContainer _container;
        
        public void Init(IActor caster, IMechanicData data, int level, DiContainer container)
        {
            _container = container;
            Caster = caster;
            Level = level;
            
            StatUpgradeFormula = _container.Resolve<IStatUpgradeFormula>();

            foreach (var chooser in data.TargetChoosersData)
            {
                var newChooser = _container.Instantiate(chooser.LogicInstanceType) as BaseTargetChooser;
                newChooser?.Init(caster, chooser);
                _choosers.Add(newChooser);
            }

            SetData(data);
        }

        public void Register(IEnumerable<ITrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.Events += ApplyMechanic;
            }
        }

        public virtual void UnRegister(IEnumerable<ITrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.Events -= ApplyMechanic;
            }
        }

        public virtual void Dispose()
        {
            
        }

        protected abstract void SetData(IMechanicData data);
        protected abstract void ApplyMechanic(object sender, EventArgs args);
    }
    
    public abstract class BaseMechanic<TData> : BaseMechanic where TData : IMechanicData
    {
        protected TData Data { get; private set; }
        
        protected override void SetData(IMechanicData data)
        {
            var mechanicData = (TData)data;
            Data = mechanicData;
            
            if (data == null)
            {
                throw new Exception();
            }
            
            OnInit();
        }
        
        protected abstract void OnInit();
    }
}