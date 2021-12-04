using System;
using Common.Actors;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public abstract class BaseMechanic<TData> : IMechanic where TData : IMechanicData
    {
        protected IActor Caster { get; private set; }
        protected TData Data { get; private set; }
        protected int Level { get; private set; }
        protected DiContainer Container { get; private set; }
        
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
            
            OnInit(caster, mechanicData);
        }

        public virtual void Dispose()
        {
            
        }
        
        protected abstract void OnInit(IActor owner, TData data);
    }
}