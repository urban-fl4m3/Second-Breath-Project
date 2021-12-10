using System.Collections.Generic;
using Common.Actors;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public abstract class BaseTargetChooser : ITargetChooser
    {
        protected IActor Owner { get; private set; }
        
        public void Init(IActor actor, ITargetChooserData data)
        {
            Owner = actor;
            SetData(data);
        }

        public abstract IEnumerable<IActor> ChooseTarget();

        protected abstract void SetData(ITargetChooserData data);
    }

    public abstract class BaseTargetChooser<T> : BaseTargetChooser where T : ITargetChooserData
    {
        protected T Data { get; private set; }
        
        protected sealed override void SetData(ITargetChooserData data)
        {
            if (data is T concreteData)
            {
                Data = concreteData;
                OnInit();
            }
        }

        protected virtual void OnInit()
        {
            
        }
    }
}