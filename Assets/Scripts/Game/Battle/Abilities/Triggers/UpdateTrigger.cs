using System;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class UpdateTrigger : BaseAbilityTrigger
    {
        private readonly IGameTickWriter _tickWriter;

        public UpdateTrigger(IGameTickWriter tickWriter)
        {
            _tickWriter = tickWriter;
        }

        public override void Dispose()
        {
            _tickWriter.RemoveTick(OnUpdate);
        }

        protected override void OnInit()
        {
            _tickWriter.AddTick(OnUpdate);
        }

        private void OnUpdate()
        {
            Trigger(EventArgs.Empty);    
        }
    }
}