using System.Collections.Generic;
using Common.Actors;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class SelfChooser : ITargetChooser
    {
        private IActor _owner;
        public List<IActor> ChooseTarget()
        {
            return new List<IActor> {_owner};
        }

        public void Init(IActor actor, ITargetChooserData data)
        {
            _owner = actor;
        }
    }
}