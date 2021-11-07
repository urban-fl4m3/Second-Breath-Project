using SecondBreath.Common.Ticks;

namespace SecondBreath.Game.Ticks
{
    public interface IGameTickHandler
    {
        void StartTicking();
        void StopTicking();
        void AddTick(ITickUpdate tick);
        void RemoveTick(ITickUpdate tick);
    }
}