using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;
using SecondBreath.Game.Battle.Abilities.Triggers;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public abstract class BaseMechanic<TData> : IMechanic where TData : IMechanicData
    {
        protected IActor Caster { get; private set; }
        protected TData Data { get; private set; }
        protected int Level { get; private set; }
        protected DiContainer Container { get; private set; }

        protected List<ITargetChooser> _choosers = new List<ITargetChooser>();
        
        public void Init(IActor caster, IMechanicData data, int level, DiContainer container)
        {
            Container = container;
            Caster = caster;
            Level = level;
            
            var mechanicData = (TData)data;
            Data = mechanicData;
            
            if (data == null)
            {
                throw new Exception();
            }

            foreach (var chooser in data.TargetChoosers)
            {
                var newChooser = Container.Instantiate(chooser.GetType()) as ITargetChooser;
                newChooser?.Init(caster);
                _choosers.Add(newChooser);
            }

            OnInit(caster, mechanicData);
        }

        public virtual void Action()
        {
            
        }

        public void Register(List<ITrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.Events += Action;
            }
        }

        public void UnRegister(List<ITrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                trigger.Events -= Action;
            }
        }

        public virtual void Dispose()
        {
            
        }
        
        protected abstract void OnInit(IActor owner, TData data);
    }
}