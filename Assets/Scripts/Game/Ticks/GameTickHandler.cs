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

        public void AddTick(ITickUpdate tick)
        {
            _tickProcessor.AddTick(tick);
        }

        public void RemoveTick(ITickUpdate tick)
        {
            _tickProcessor.RemoveTick(tick);
        }
    }
}