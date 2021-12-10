using System;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class OnDeathTrigger : BaseAbilityTrigger
    {
        public override void Dispose()
        {
            Actor.Killed -= Action;
        }

        protected override void OnInit()
        {
            Actor.Killed += Action;
        }
        
        private void Action(object sender, EventArgs args)
        {
            Trigger(args);
        }
    }
}