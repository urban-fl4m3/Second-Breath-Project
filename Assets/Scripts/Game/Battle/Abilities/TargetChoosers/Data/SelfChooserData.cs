using System;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class SelfChooserData : ITargetChooserData
    {
        public virtual Type LogicInstanceType => typeof(SelfChooser);
    }
}