using System;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    [Serializable]
    public class SelfChooserData : ITargetChooserData
    {
        public virtual Type LogicInstanceType => typeof(SelfChooser);
    }
}