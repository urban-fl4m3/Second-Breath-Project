using System.Collections.Generic;
using Common.Actors;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class SelfChooser : BaseTargetChooser<SelfChooserData>
    {
        public override IEnumerable<IActor> ChooseTarget()
        {
            return new List<IActor> { Owner };
        }
    }
}