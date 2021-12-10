using System;
using SecondBreath.Common.Ticks;

namespace SecondBreath.Game.Ticks
{
    public class GameTickHandler : IGameTickHandler
    {
        private readonly ITickProcessor _tickProcessor = new TickProcessor();
        
        public void StartTicking()
        {
            _tickProcessor.Start();
        }

        public void StopTicking()
        {
            _tickProcessor.Stop();
        }

        public void AddTick(Action tick)
        {
            _tickProcessor.AddTick(tick);
        }

        public void RemoveTick(Action tick)
        {
            _tickProcessor.RemoveTick(tick);
        }
    }
}